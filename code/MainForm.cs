using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EncryptionHeckingCode
{
    public partial class EncryptionMainForm : Form
    {
        public EncryptionMainForm()
        {
            InitializeComponent();
        }

        private List<Encryptor> arr = new List<Encryptor>();
        public event EventHandler EncryptionComplete;


        public enum EncryptionType
        {
            None,
            AES,
            RSA,
            TripleDES,
            Blowfish,
            Twofish
        }


        private void hideEncryptionKeyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            encryptionKeyTextbox.UseSystemPasswordChar = checkBox.Checked;
        }

        private void GenerateRandomKeyButton_Click(object sender, EventArgs e)
        {
            if (EncryptButton.Text == "Stop")
            {
                foreach (Encryptor f in arr)
                {
                    f.Stop();
                    EncryptButton.Text = "Encrypt";
                    arr.Remove(f);
                }
            }
            else
            {
                
                Encryptor E = null;

                EncryptionType Type = EncryptionType.None;
                Type = (EncryptionType) Enum.Parse(typeof(EncryptionType), encryptionMethodComboBox.Text, true);

                //AES
                if (Type == EncryptionType.AES)
                {
                    if (RandomKeyCheckBox.Checked)
                    {
                        //start with random generated key
                        E = new AES();
                    }
                    else
                    {
                        //start with user input key
                        E = new AES(encryptionKeyTextbox.Text);
                        //check that the new key is accepted by AES
                    }
                    if (!E.ValidateKey())
                    {
                        MessageBox.Show("Key length must be greater than 128 and a multiple of 32");
                        E = null;
                    }


                }

                //RSA
                if (Type == EncryptionType.RSA)
                {
                    MessageBox.Show("Being developed soon!");
                }
                //TripleDes
                if (Type == EncryptionType.TripleDES)
                {
                    MessageBox.Show("Being developed soon!");
                }
                //Blowfish
                if (Type == EncryptionType.Blowfish)
                {
                    MessageBox.Show("Being developed soon!");
                }
                //TwoFish
                if (Type == EncryptionType.Twofish)
                {
                    MessageBox.Show("Being developed soon!");
                }


                if (E != null)
                {
                    E.Start(encryptRadioButton.Checked);
                    EncryptButton.Text = "Stop";
                    arr.Add(E);
                   
                   
                    //when e.complete = true
                    E.Output(ref ciphertextTextbox);
                }

            }

        }

        internal static void Output(object sender, ProgressChangedEventArgs e)
        {
            //update progress bar
            MessageBox.Show("Im in the method");
        }

       
       
        private void CheckToEnableEncryptButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(plaintextTextbox.Text) &&
                encryptionKeyTextbox.Text != "")
            {
                encryptRadioButton.Enabled = true;
                decryptRadioButton.Enabled = true;
            }
            else
            {
                encryptRadioButton.Enabled = false;
                decryptRadioButton.Enabled = false;
            }
        }

        private void EncryptionMainForm_Load(object sender, EventArgs e)
        {
            encryptionMethodComboBox.HighlightColor = Color.White;
            encryptionMethodComboBox.SelectedIndex = 0;
        }

        private void encryptionMethodComboBox_DropDown(object sender, EventArgs e)
        {
            encryptionMethodComboBox.HighlightColor = Color.Gainsboro;
        }
    }
}
