using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionHeckingCode
{
    class AES : Encryptor
    {
        
        private Key key;
        

       
       public AES()
        {
            //called if random checkbox is ticked meaning  we genertae a key 
                //just for now assume length is 128 (must be multiple of 32) and alphanumeric is true until UI updated 
               key = new Key(128, true); 
        }

        public AES(string input)
        {
            //called if the user wants to input a string to be their key

            key = new Key(input);
        }


        public override bool ValidateKey()
        {
            if (key.Keylength >= 128 && (key.Keylength % 32) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        public override void Start(bool Enc)
        {
            this.bw = new BackgroundWorker();
            if (Enc)
            {
                this.bw.DoWork += Encrypt;
            }
            else
            {
                this.bw.DoWork += Decrypt;
            }
            this.bw.RunWorkerAsync();
            this.bw.WorkerSupportsCancellation = true;
            this.bw.WorkerReportsProgress = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(EncryptionMainForm.Output);
            
        }

        public override void Stop()
        {
            this.bw.CancelAsync();
        }

        protected override void Encrypt(object sender, EventArgs e)
        {
            int PerCentComplete = 100;
            this.bw.ReportProgress(PerCentComplete);
            
        }

        protected override void Decrypt(object sender, EventArgs e)
        {

        }

       
    }
}
