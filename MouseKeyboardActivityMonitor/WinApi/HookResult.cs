using System;

namespace MouseKeyboardActivityMonitor.WinApi
{
    internal class HookResult : IDisposable
    {
        private readonly HookProcedureHandle m_Handle;
        private readonly HookProcedure m_Procedure;

        public HookResult(HookProcedureHandle handle, HookProcedure procedure)
        {
            m_Handle = handle;
            m_Procedure = procedure;
        }

        public HookProcedureHandle Handle
        {
            get { return m_Handle; }
        }

        public HookProcedure Procedure
        {
            get { return m_Procedure; }
        }

        public void Dispose()
        {
            m_Handle.Dispose();
        }
    }
}