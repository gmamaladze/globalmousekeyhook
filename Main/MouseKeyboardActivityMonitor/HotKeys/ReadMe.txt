Until a separate, full-featured test version is ready, here's a quick update that can be made to the TestFormHookListeners:


		public TestFormHookListeners()
        {

            InitializeComponent();
            //m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            //m_KeyboardHookManager.Enabled = true;

            m_MouseHookManager = new MouseHookListener(new GlobalHooker());
            m_MouseHookManager.Enabled = true;

            HotKeySetCollection hkscoll = new HotKeySetCollection();
            m_KeyboardHookManager = new HotKeySetsListener( hkscoll, new GlobalHooker() ) { Enabled = true };

            BuildHotKeyTests( hkscoll );
        }

        private void BuildHotKeyTests( HotKeySetCollection hkscoll )
        {
            hkscoll.Add( BindHotKeySet( new[] { Keys.T, Keys.LShiftKey }, null, OnHotKeyDownOnce1, OnHotKeyDownHold1, OnHotKeyUp1, "test1" ) );
            hkscoll.Add( BindHotKeySet( new[] { Keys.T, Keys.LControlKey, Keys.RControlKey }, new [] { Keys.LControlKey, Keys.RControlKey }, OnHotKeyDownGeneral2, OnHotKeyDownGeneral2, OnHotKeyUp1, "test2" ) );
        }

        private static HotKeySet BindHotKeySet( IEnumerable< Keys > ks, 
                                                IEnumerable< Keys > xorKeys,
                                                HotKeySet.HotKeyHandler onEventDownOnce,
                                                HotKeySet.HotKeyHandler onEventDownHold,
                                                HotKeySet.HotKeyHandler onEventUp,
                                                string name )
        {
            
            HotKeySet hks = new HotKeySet( ks );

            if ( xorKeys != null )
                hks.RegisterExclusiveOrKey( xorKeys );

            hks.OnHotKeysDownOnce += onEventDownOnce;
            hks.OnHotKeysDownHold += onEventDownHold;
            hks.OnHotKeysUp += onEventUp;

            hks.Name = ( name ?? String.Empty );

            return hks;

        }

        private void OnHotKeyDownGeneral2( HotKeySet sender )
        {
            string kstring = String.Join( ", ", sender.HotKeys );
            textBoxLog.AppendText( "ONCE/HOLD: " + sender.Name + ": " + kstring + Environment.NewLine );
        }

        private void OnHotKeyDownOnce1( HotKeySet sender )
        {
            string kstring = String.Join( ", ", sender.HotKeys );
            textBoxLog.AppendText( "ONCE: " + sender.Name + ": " + kstring + Environment.NewLine );
        }

        private void OnHotKeyDownHold1( HotKeySet sender )
        {
            string kstring = String.Join( ", ", sender.HotKeys );
            textBoxLog.AppendText( "HOLD: " + sender.Name + ": " + kstring + Environment.NewLine );
        }

        private void OnHotKeyUp1( HotKeySet sender )
        {
            string kstring = String.Join( ", ", sender.HotKeys );
            textBoxLog.AppendText( "UP: " + sender.Name + ": " + kstring + Environment.NewLine );
        }



