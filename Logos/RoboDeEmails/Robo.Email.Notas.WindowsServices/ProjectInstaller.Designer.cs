namespace Robo.Email.Notas.WindowsServices
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Robo = new System.ServiceProcess.ServiceProcessInstaller();
            this.EnviaEmailNoatasFiscais = new System.ServiceProcess.ServiceInstaller();
            // 
            // Robo
            // 
            this.Robo.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.Robo.Password = null;
            this.Robo.Username = null;
            // 
            // EnviaEmailNoatasFiscais
            // 
            this.EnviaEmailNoatasFiscais.Description = "Robo de Notas Fiscais em Aberto";
            this.EnviaEmailNoatasFiscais.DisplayName = "Robo de Notas Fiscais em Aberto";
            this.EnviaEmailNoatasFiscais.ServiceName = "Service1";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.Robo,
            this.EnviaEmailNoatasFiscais});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller Robo;
        private System.ServiceProcess.ServiceInstaller EnviaEmailNoatasFiscais;
    }
}