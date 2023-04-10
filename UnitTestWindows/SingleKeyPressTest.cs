using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Linq;

namespace UnitTestWindows
{
    [TestClass]
    public class SingleKeyPressTest
    {

        private readonly Channel<KeyPressEventArgs> channel = Channel.CreateUnbounded<KeyPressEventArgs>();

        [TestInitialize]
        public void TestInitilize()
        {
            Hook.GlobalEvents().KeyPress += (sender, e) =>
            {
                var canWrite = channel.Writer.TryWrite(e);
                Assert.IsTrue(canWrite);
            };
        }

        [TestMethod]
        public async Task TestOneKeystroke()
        {
            var expected = 'a';

            // Simulate a keystroke by sending the letter 'a'
            SendKeys.SendWait(expected.ToString());

            // Wait for the keystroke event to arrive in the channel
            var eventArgs = await channel.Reader.ReadAsync();
            var actual = eventArgs.KeyChar;

            // Ensure that the event contains the correct character
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TestAllRegularKeystrokes()
        {
            var actual = "abcdefghijklmnopqrstuvwxyz1234567890`-=[]\\;',./";
            var expected = "";

            // Simulate keystrokes for all regular and special keys
            foreach (var key in actual)
            {
                // Send the keystroke using SendKeys.SendWait
                SendKeys.SendWait("{" + key + "}");
                // Wait for the keystroke event to arrive in the channel
                var eventArgs = await channel.Reader.ReadAsync();
                // Append the key to the expected string
                expected += eventArgs.KeyChar;
            }
            Assert.AreEqual(expected, actual);
        }
    }
}