using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TreagleLocal
{
    public partial class Form1 : Form
    {

        private TextBox sideATextBox;
        private TextBox sideBTextBox;
        private TextBox sideCTextBox;
        private Button sendButton;
        private Label resultLabel;

        public Form1()
        {
            InitializeComponent();
            // ������� �������� ����������
            // ������� �������� ����������
            sideATextBox = new TextBox();
            sideATextBox.Location = new System.Drawing.Point(10, 10);
            sideATextBox.Size = new System.Drawing.Size(100, 30);

            sideBTextBox = new TextBox();
            sideBTextBox.Location = new System.Drawing.Point(10, 40);
            sideBTextBox.Size = new System.Drawing.Size(100, 30);

            sideCTextBox = new TextBox();
            sideCTextBox.Location = new System.Drawing.Point(10, 70);
            sideCTextBox.Size = new System.Drawing.Size(100, 30);

            sendButton = new Button();
            sendButton.Location = new System.Drawing.Point(10, 100);
            sendButton.Size = new System.Drawing.Size(100, 60);
            sendButton.Text = "���������";
            sendButton.Click += SendButton_Click;

            resultLabel = new Label();
            resultLabel.Location = new System.Drawing.Point(10, 170);
            resultLabel.AutoSize = true;

            // ��������� �������� �� �����
            Controls.Add(sideATextBox);
            Controls.Add(sideBTextBox);
            Controls.Add(sideCTextBox);
            Controls.Add(sendButton);
            Controls.Add(resultLabel);

            // ������������� �������� �����
            Text = "���������� ����������";
            Size = new System.Drawing.Size(520, 520);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            // ������������� IP-����� � ���� �������
            string serverIP = "127.168.100.9"; // ��������� IP-����� �������
            int serverPort = 1024; // ���� �������

            // ������� ���������� ����� � ������������ � �������
            using (TcpClient client = new TcpClient(serverIP, serverPort))
            {
                // ������ ��� ����� ������ ������������ �� TextBox'��
                double sideA = double.Parse(sideATextBox.Text);
                double sideB = double.Parse(sideBTextBox.Text);
                double sideC = double.Parse(sideCTextBox.Text);

                // ���������� ����� ������ �� ������
                string dataToSend = sideA + "," + sideB + "," + sideC;
                byte[] dataBytes = Encoding.ASCII.GetBytes(dataToSend);
                NetworkStream networkStream = client.GetStream();
                networkStream.Write(dataBytes, 0, dataBytes.Length);

                // �������� ��������� �� �������
                byte[] buffer = new byte[1024];
                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                string resultFromServer = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // ���������� ��������� �� �����
                resultLabel.Text = "������� ������������: " + resultFromServer;
            }
        }

    }
}