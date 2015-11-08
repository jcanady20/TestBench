using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;

namespace TestBench
{
	public class SampleService : ServiceBase
	{
		private Thread m_worker;
		private bool m_cancel;
		private bool m_paused;
		private EventLog m_eventLog;

		public static readonly string EVENTLOGNAME = "SampleService";
		public static readonly string EVENTLOGSOURCE = "SampleService";

		public SampleService()
		{
			this.CanShutdown = true;
			this.CanStop = true;
			this.CanPauseAndContinue = true;
			this.CanHandlePowerEvent = false;
			this.CanHandleSessionChangeEvent = false;
			
			this.m_cancel = false;
		}
		public void Start()
		{
			m_cancel = false;
			if(m_worker != null && m_worker.IsAlive)
			{
				return;
			}
			m_worker = new Thread(new ThreadStart(DoWork));
			m_worker.Start();
		}
		public new void Stop()
		{
			m_cancel = true;
			base.Stop();
		}
		protected override void OnStart(string[] args)
		{
			this.Start();
			base.OnStart(args);
		}
		protected override void OnContinue()
		{
			this.m_paused = false;
			base.OnContinue();
		}
		protected override void OnPause()
		{
			this.m_paused = true;
			base.OnPause();
		}
		private void DoWork()
		{
			//	While/true are evil but in a service loops, its allowed
			while(true)
			{
				if(m_cancel)
				{
					break;
				}

				if(m_paused)
				{
					//	Sleep and then skip doing actual work
					Sleep(1000);
					continue;
				}

				//	Actual Work Stuff



				// Give the other things a changes to do things
				Sleep(1000);
			}
		}
		private void Sleep(int timeOut)
		{
			Thread.Sleep(timeOut);
		}
		public override System.Diagnostics.EventLog EventLog
		{
			get
			{
				if (m_eventLog == null)
				{
					m_eventLog = new EventLog(EVENTLOGNAME, Environment.MachineName, EVENTLOGSOURCE);
				}
				return m_eventLog;
			}
		}
	}
}