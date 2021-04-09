namespace RepService
{
    partial class RepService
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GRCCRepLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.GRCCRepLog)).BeginInit();
            // 
            // GRCCRepLog
            // 
            this.GRCCRepLog.Source = "GRCCRepService";
            // 
            // RepService
            // 
            this.ServiceName = "RepService";
            ((System.ComponentModel.ISupportInitialize)(this.GRCCRepLog)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog GRCCRepLog;
    }
}
