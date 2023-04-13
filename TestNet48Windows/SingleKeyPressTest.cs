using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using NUnit.Framework;

namespace UnitTestWindows
{


    public class SingleKeyPressTest
    {

        private Channel<KeyPressEventArgs> channel;
        private AutoResetEvent handle;
        private string buffer;

        [SetUp]
        public void Setup()
        {
            channel = Channel.CreateUnbounded<KeyPressEventArgs>();
            Hook.GlobalEvents().KeyPress += OnKeyPress;
            handle = new AutoResetEvent(false);
            buffer= string.Empty;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            //var canWrite = channel.Writer.TryWrite(e);
            //Assert.True(canWrite);
            System.Console.WriteLine(e.KeyChar);
            e.Handled = true;
            buffer += e.KeyChar;
            handle.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            Hook.GlobalEvents().KeyPress -= OnKeyPress;
            this.channel.Writer.Complete();
        }

        [Test]
        public void TestAllRegularKeystrokes()
        {
            var expected = "abcdefghijklmnopqrstuvwxyz1234567890`-=[]\\;',./";

            // Simulate keystrokes for all regular and special keys
            foreach (var key in expected)
            {
                handle.Set();
                // Send the keystroke using SendKeys.SendWait
                SendKeys.SendWait("{" + key + "}");
                Application.DoEvents();
                handle.WaitOne(100);
            }
            var actual = buffer;
            Assert.AreEqual(expected, actual);
        }
    }
}