using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace TestBench
{
	[RunInstaller(true)]
	public class ServiceInstall : System.Configuration.Install.Installer
	{
		public ServiceInstall()
		{
			ServiceProcessInstaller process = new ServiceProcessInstaller();
			ServiceInstaller serviceAdmin = new ServiceInstaller();

			process.Account = ServiceAccount.LocalSystem;

			serviceAdmin.StartType = ServiceStartMode.Automatic;
			serviceAdmin.ServiceName = "Sample Service";
			serviceAdmin.DisplayName = "Sample Service";
			serviceAdmin.Description = "This is a sample Service installed by TestBench. Use TestBench UnInstall to remove this Service";

			Installers.Add(process);
			Installers.Add(serviceAdmin);

		}
	}
}
