using System;
using System.Windows.Forms;
using Gma.UserActivityMonitor.WinApi;

namespace Gma.UserActivityMonitor
{
	/// <summary>
	/// This class monitors all mouse activities and provides appropriate events.
	/// </summary>
	public class MouseHookListener : BaseHookListener
	{
		private readonly DoubleClickDetector m_DoubleClickDetector;

		private Point m_PreviousPosition = new Point(0,0);
		private MouseButtons m_SuppressButtonUpFlags = MouseButtons.None;

		/// <summary>
		/// Initializes a new instance of <see cref="MouseHookListener"/>.
		/// </summary>
		/// <param name="hooker">Depending on this parameter the listener hooks either application or global mouse events.</param>
		/// <remarks>Hooks are not active after instantiation. You need to use either <see cref="BaseHookListener.Enabled"/> property or call <see cref="BaseHookListener.Start"/> method.</remarks>
		public MouseHookListener(Hooker hooker) 
			: this(hooker, new DoubleClickDetector())
		{
			
		}
		
		private MouseHookListener(Hooker hooker, DoubleClickDetector doubleClickDetector) 
			: base(hooker)
		{
			m_DoubleClickDetector = doubleClickDetector;
		}

		/// <summary>
		/// This method processes the data from the hook and initiates event firing.
		/// </summary>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		protected override bool ProcessCallback(int wParam, IntPtr lParam)
		{
			MouseEventExtArgs e = MouseEventExtArgs.FromRawData(wParam, lParam, IsGlobal);

			if (e.IsMouseKeyDown)
			{
			    e = m_DoubleClickDetector.OnMouseDown(e);
				InvokeMouseEventHandler(MouseDown, e);
				InvokeMouseEventHandlerExt(MouseDownExt, e);
				if (e.Handled)
				{
					SetSupressButtonUpFlag(e.Button);
					e.Handled = true;
				}
			}

			if (e.Clicks == 2 && e.IsMouseKeyDown && !e.Handled)
			{
				InvokeMouseEventHandler(MouseDoubleClick, e);
			}

			if (e.Clicks==1 && e.IsMouseKeyUp && !e.Handled)
			{
				InvokeMouseEventHandler(MouseClick, e);
				InvokeMouseEventHandlerExt(MouseClickExt, e);
			}

			if (e.IsMouseKeyUp)
			{
				if (!HasSupressButtonUpFlag(e.Button))
				{
					InvokeMouseEventHandler(MouseUp, e);
				}
				else
				{
					RemoveSupressButtonUpFlag(e.Button);
					e.Handled = true;
				}
			}

			if (e.WheelScrolled)
			{
				InvokeMouseEventHandler(MouseWheel, e);
			}

			if (HasMoved(e.Point))
			{
				m_PreviousPosition = e.Point;

				InvokeMouseEventHandler(MouseMove, e);
				InvokeMouseEventHandlerExt(MouseMoveExt, e);
			}

			return e.Handled;
		}

		private void RemoveSupressButtonUpFlag(MouseButtons button)
		{
			m_SuppressButtonUpFlags = m_SuppressButtonUpFlags ^ button;
		}

		private bool HasSupressButtonUpFlag(MouseButtons button)
		{
			return (m_SuppressButtonUpFlags & button) != 0;
		}

		private void SetSupressButtonUpFlag(MouseButtons button)
		{
			m_SuppressButtonUpFlags = m_SuppressButtonUpFlags | button;
		}

		/// <summary>
		/// Returns the correct hook id to be used for <see cref="Hooker.SetWindowsHookEx"/> call.
		/// </summary>
		/// <returns></returns>
		protected override int GetHookId()
		{
			return IsGlobal ?
				GlobalHooker.WH_MOUSE_LL :
				AppHooker.WH_MOUSE;
		}

		private bool HasMoved(Point actualPoint)
		{
			return m_PreviousPosition == actualPoint;
		}

		private void InvokeMouseEventHandler(MouseEventHandler handler, MouseEventArgs e)
		{
			if (handler != null)
			{
				handler(this, e);
			}
		}

		
		private void InvokeMouseEventHandlerExt(EventHandler<MouseEventExtArgs> handler, MouseEventExtArgs e)
		{
			if (handler != null)
			{
				handler(this, e);
			}
		}

		/// <summary>
		/// Occurs when the mouse pointer is moved.
		/// </summary>
		public event MouseEventHandler MouseMove;

		/// <summary>
		/// Occurs when the mouse pointer is moved.
		/// </summary>
		/// <remarks>
		/// This event provides extended arguments of type <see cref = "MouseEventArgs" /> enabling you to 
		/// supress further processing of mouse movement in other applications.
		/// </remarks>
		public event EventHandler<MouseEventExtArgs> MouseMoveExt;

		/// <summary>
		/// Occurs when a click was performed by the mouse.
		/// </summary>
		public event MouseEventHandler MouseClick;

		/// <summary>
		/// Occurs when a click was performed by the mouse.
		/// </summary>
		/// <remarks>
		/// This event provides extended arguments of type <see cref = "MouseEventArgs" /> enabling you to 
		/// supress further processing of mouse click in other applications.
		/// </remarks>
		[Obsolete("To supress mouse clicks use MouseDownExt event instead.")]
		public event EventHandler<MouseEventExtArgs> MouseClickExt;

		/// <summary>
		/// Occurs when the mouse a mouse button is pressed.
		/// </summary>
		public event MouseEventHandler MouseDown;

		/// <summary>
		/// Occurs when the mouse a mouse button is pressed.
		/// </summary>
		/// <remarks>
		/// This event provides extended arguments of type <see cref = "MouseEventArgs" /> enabling you to 
		/// supress further processing of mouse click in other applications.
		/// </remarks>
		public event EventHandler<MouseEventExtArgs> MouseDownExt;

		/// <summary>
		/// Occurs when a mouse button is released.
		/// </summary>
		public event MouseEventHandler MouseUp;

		/// <summary>
		/// Occurs when the mouse wheel moves.
		/// </summary>
		public event MouseEventHandler MouseWheel;

	    /// <summary>
	    /// TODO
	    /// </summary>
	    public event MouseEventHandler MouseDoubleClick;

	    /// <summary>
		/// Release delegates, unsubscribes from hooks.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public override void Dispose()
		{
			m_DoubleClickDetector.Dispose();
			MouseClick = null;
			MouseClickExt = null;
			MouseDown = null;
			MouseDownExt = null;
			MouseMove = null;
			MouseMoveExt = null;
			MouseUp = null;
			MouseWheel = null;
			base.Dispose();
		}
	}
}