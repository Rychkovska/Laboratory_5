using Laboratory_5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_5
{
	public partial class fMain : Form
	{
		public fMain()
		{
			InitializeComponent();
			this.Width = 990;
			this.Height = 400;
		}

		private void fMain_Load(object sender, EventArgs e)
		{
			gvBooks.AutoGenerateColumns = false;

			DataGridViewColumn column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "title";
			column.Name = "Назва";
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "author";
			column.Name = "Автор";
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "NumPages";
			column.Name = "Кільість сторінок";
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "WordCount"; 
			column.Name = "Загальна кількість слів";
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "publisher";
			column.Name = "Видавництво";
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "yearpublished";
			column.Name = "Рік опублікування";
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "language";
			column.Name = "Мова";
			column.Width = 60;
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "genre";
			column.Name = "Жанр";
			column.Width = 60;
			gvBooks.Columns.Add(column);

			column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = "WordsPerPage"; 
			column.Name = "Кількість слів на одній сторінці";
			gvBooks.Columns.Add(column);

			column = new DataGridViewCheckBoxColumn();
			column.DataPropertyName = "BigorSmall";
			column.Name = "Книжка велика";
			gvBooks.Columns.Add(column);


			bindSrcBooks.Add(new Book ("Фах","Айзек Азімов", 40, 12000, "Astounding Science Fiction", 1957,
				"англійська", "наукова фантастика", 300, false));
			EventArgs args = new EventArgs(); OnResize(args);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			Book book = new Book();

			fBook ft = new fBook(book);
			if (ft.ShowDialog() == DialogResult.OK)
			{
				bindSrcBooks.Add(book);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			Book book = (Book)bindSrcBooks.List[bindSrcBooks.Position];

			fBook fb = new fBook(book);
			if (fb.ShowDialog() == DialogResult.OK)
			{
				bindSrcBooks.List[bindSrcBooks.Position] = book;
			}
		}


		private void btnDel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Видалити поточний запис?", "Видалення запису",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				bindSrcBooks.RemoveCurrent();

			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				bindSrcBooks.Clear();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Закрити застосунок?", "Вихід з програми", MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question) == DialogResult.OK)
			{
				Application.Exit();
			}
		}

		private void fMain_Resize(object sender, EventArgs e)
		{
			int buttonsSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width + 30;
			btnExit.Margin = new Padding(Width - buttonsSize, 0, 0, 0);
		}

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
			saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
			saveFileDialog.Title = "Зберегти дані у текстовому форматі";
			saveFileDialog.InitialDirectory = Application.StartupPath;
			StreamWriter sw;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
				try
				{
					foreach (Book book in bindSrcBooks.List)
					{
						sw.Write(book.Title + "\t" + book.Author + "\t" + book.NumPages + "\t" +
							book.WordCount + "\t" + book.Publisher + "\t" + book.YearPublished + "\t" +
							book.Language + "\t" + book.Genre + "\t" + book.Onthepage + "\t" + book.SizeofBook + "\t\n");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Сталась помилка: \n{0}", ex.Message, 
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally { sw.Close(); }
			}
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних (*.books)|*.books|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
			BinaryWriter bw;
			if (saveFileDialog.ShowDialog() == DialogResult.OK )
			{
				bw = new BinaryWriter (saveFileDialog.OpenFile());
				try
				{
					foreach (Book book in bindSrcBooks.List)
					{
						bw.Write(book.Title);
						bw.Write(book.Author);
						bw.Write(book.NumPages);
						bw.Write(book.WordCount);
						bw.Write(book.Publisher);
						bw.Write(book.YearPublished);
						bw.Write(book.Language);
						bw.Write(book.Genre);
						bw.Write(book.Onthepage);
						bw.Write(book.SizeofBook);
					}
				}
				catch (IOException ex) 
				{ 
					MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally { bw.Close(); }
			}
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
			StreamReader sr;
			if (openFileDialog.ShowDialog() == DialogResult.OK )
			{
				bindSrcBooks.Clear();
				sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
				string s;
				try
				{
					while ((s=sr.ReadLine()) != null)
					{
						string[] split = s.Split('\t');
						Book book = new Book(split[0], split[1], int.Parse(split[2]), int.Parse(split[3]),
							split[4], int.Parse(split[5]), split[6], split[7], int.Parse(split[8]), bool.Parse(split[9]));
						bindSrcBooks.Add(book);
					}
				}
				catch (Exception ex) 
				{ 
					MessageBox.Show("Сталась помилка: \n{0}", ex.Message, 
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally { sr.Close(); }
			}
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файли даних (*.books)|*.books|All files (*.*)|*.*";
            openFileDialog.Title = "Прочитати дані у бінарному форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
			BinaryReader br;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				bindSrcBooks.Clear();
				br = new BinaryReader(openFileDialog.OpenFile());
				try
				{
					Book book;
					while (br.BaseStream.Position < br.BaseStream.Length)
					{
						book = new Book();
						for (int i=1; i<=10; i++)
						{
							switch (i)
							{
								case 1:
									book.Title = br.ReadString(); break;
								case 2:
									book.Author = br.ReadString(); break;
								case 3:
									book.NumPages = br.ReadInt32(); break;
								case 4:
									book.WordCount = br.ReadInt32(); break;
								case 5:
									book.Publisher = br.ReadString(); break;
								case 6:
									book.YearPublished = br.ReadInt32(); break;
								case 7:
									book.Language = br.ReadString(); break;
								case 8:
									book.Genre = br.ReadString(); break;
								case 9:
									book.Onthepage = br.ReadInt32(); break;
								case 10:
									book.SizeofBook = br.ReadBoolean(); break;
							}
						}
						bindSrcBooks.Add(book);
					}
				}
				catch (Exception ex) 
				{ 
					MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally { br.Close(); }
			}
        }
    }
}
