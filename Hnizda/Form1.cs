namespace Hnizda
{
    public partial class Form1 : Form
    {
        List<Hnizdo> hnizda = new List<Hnizdo>();
        public Form1()
        {
            InitializeComponent();
        }

        private void ReloadDraw(Graphics g)
        {
            foreach (var h in hnizda)
            {
                g.DrawEllipse(new Pen(Brushes.White, 10f), h.x - h.width / 2, h.y - h.height / 2, h.width, h.height);
                g.DrawString(h.name, new Font("Arial", 12f), Brushes.White, h.x - h.width / 4, h.y - h.height / 4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists("iamthebiggestbird.txt"))
                File.Create("iamthebiggestbird.txt");
            string textik = "";
            foreach (var h in hnizda)
            {
                textik = textik + h.name + "," + h.x + "," + h.y + ",\n";
            }

            File.WriteAllText("iamthebiggestbird.txt", textik);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadText();
        }

        private void ReadText()
        {
            hnizda.Clear();
            using (StreamReader reader = new StreamReader("iamthebiggestbird.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');
                    Hnizdo hnizdo = new Hnizdo(80, 40);
                    hnizdo.name = parts[0];
                    hnizdo.x = int.Parse(parts[1]);
                    hnizdo.y = int.Parse(parts[2]);
                    hnizda.Add(hnizdo);
                }
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ReloadDraw(e.Graphics);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Point p = pictureBox1.PointToClient(Cursor.Position);
                Hnizdo h = new Hnizdo(80, 40);
                h.x = p.X;
                h.y = p.Y;
                h.name = GetTextBox();
                hnizda.Add(h);
                Refresh();
            }

            if (e.Button == MouseButtons.Right)
            {
                hnizda.RemoveAt(hnizda.Count - 1);
                Refresh();
            }
        }

        private string GetTextBox()
        {
            if (textBox1.Text != "")
                return textBox1.Text;

            else
                return "Bird";
        }
    }
}