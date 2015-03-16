using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.ComponentModel;

namespace TestBench
{
	public class ServiceController : IDisposable
	{
		private string m_serviceName = "ApplixSyncService";
		private ManagementObject m_managementObject;

		public ServiceController()
		{
			string objPath = String.Format("Win32_Service.Name='{0}'", m_serviceName);
			m_managementObject = new ManagementObject(new ManagementPath(objPath));
		}

		public ServiceController(string serviceName)
		{
			string objPath = String.Format("Win32_Service.Name='{0}'", serviceName);
			m_managementObject = new ManagementObject(new ManagementPath(objPath));
		}

		public void StartService()
		{
			if (m_managementObject == null)
				return;
			m_managementObject.InvokeMethod("StartService", null, null);
		}

		public void StopService()
		{
			if (m_managementObject == null)
				return;
			m_managementObject.InvokeMethod("StopService", null, null);
		}

		public void PauseService()
		{
			if (m_managementObject == null)
				return;
			m_managementObject.InvokeMethod("PauseService", null, null);
		}

		public void ResumeService()
		{
			if (m_managementObject == null)
				return;

			m_managementObject.InvokeMethod("ResumeService", null, null);
		}

		public void ChangeServiceCredentials(string userName, string password)
		{
			if (m_managementObject == null)
				return;

			object[] wmiParams = new object[11];
			wmiParams[6] = userName;
			wmiParams[7] = password;

			m_managementObject.InvokeMethod("Change", wmiParams);
		}

		#region Private Fields
		private bool m_canPause;
		private bool m_canStop;
		private string m_caption;
		private string m_description;
		private bool m_desktopInteract;
		private string m_displayName;
		private DateTime m_installDate;
		private string m_name;
		private string m_pathName;
		private UInt32 m_processId;
		private bool m_started;
		private string m_startMode;
		private string m_startName;
		private string m_state;
		private string m_status;
		private string m_systemName;
		#endregion

		#region Public ReadOnly Properties
		public bool CanPause
		{
			get
			{
				if (m_canPause == null)
				{
					m_canPause = GetValue<bool>("AcceptPause");
				}
				return m_canPause;
			}
		}

		public bool CanStop
		{
			get
			{
				if (m_canStop == null)
				{
					m_canStop = GetValue<bool>("AcceptStop");
				}
				return m_canStop;
			}
		}

		public string Caption
		{
			get
			{
				if (String.IsNullOrEmpty(m_caption))
				{
					m_caption = GetValue<String>("Caption");
				}
				return m_caption;
			}
		}

		public string Deascription
		{
			get
			{
				if (String.IsNullOrEmpty(m_description))
				{
					m_description = GetValue<String>("Description");
				}
				return m_description;
			}
		}

		public bool DeskTopInteraction
		{
			get
			{
				if (m_desktopInteract == null)
				{
					m_desktopInteract = GetValue<bool>("DesktopInteract");
				}
				return m_desktopInteract;
			}
		}

		public string DisplayName
		{
			get
			{
				if (String.IsNullOrEmpty(m_displayName))
				{
					m_displayName = GetValue<String>("DisplayName");
				}
				return m_displayName;
			}
		}

		public DateTime InstallDate
		{
			get
			{
				if (m_installDate == null || m_installDate != DateTime.MinValue)
				{
					m_installDate = GetValue<DateTime>("InstallDate");
				}
				return m_installDate;
			}
		}

		public string Name
		{
			get
			{
				if (String.IsNullOrEmpty(m_name))
				{
					m_name = GetValue<String>("Name");
				}
				return m_name;
			}
		}

		public string ExecutionPath
		{
			get
			{
				if (String.IsNullOrEmpty(m_pathName))
				{
					m_pathName = GetValue<String>("PathName");
				}
				return m_pathName;
			}
		}

		public UInt32 ProcessID
		{
			get
			{
				if (m_processId == null)
				{
					m_processId = GetValue<UInt32>("ProcessId");
				}
				return m_processId;
			}
		}

		public bool IsStarted
		{
			get
			{
				if (m_started == null)
				{
					m_started = GetValue<bool>("Started");
				}
				return m_started;
			}
		}

		public string StartupMode
		{
			get
			{
				if (String.IsNullOrEmpty(m_startMode))
				{
					m_startMode = GetValue<String>("StartMode");
				}
				return m_startMode;
			}
		}

		public string StartName
		{
			get
			{
				if (String.IsNullOrEmpty(m_startName))
				{
					m_startName = GetValue<String>("StartName");
				}
				return m_startName;
			}
		}

		public string State
		{
			get
			{
				if (String.IsNullOrEmpty(m_state))
				{
					m_state = GetValue<String>("State");
				}
				return m_state;
			}
		}

		public string Status
		{
			get
			{
				if (String.IsNullOrEmpty(m_status))
				{
					m_status = GetValue<String>("Status");
				}
				return m_status;
			}
		}
		#endregion

		private T GetValue<T>(string propertyName)
		{
			if (m_managementObject == null)
				return default(T);

			object o = m_managementObject.GetPropertyValue(propertyName);

			if (o == null)
				return default(T);

			if (o is T)
			{
				return (T)o;
			}

			return default(T);
		}

		public void Dispose()
		{
			if (m_managementObject != null)
			{
				m_managementObject.Dispose();
			}
		}
	}
}
