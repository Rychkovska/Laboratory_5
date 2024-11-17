using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_5
{
	public partial class fBook : Form
	{
		public fBook(Book t)
		{
			TheBook = t;
			InitializeComponent();
		}

		public Book TheBook;

		private void fBook_Load(object sender, EventArgs e)
		{
			if (TheBook != null)
			{
				tbName.Text = TheBook.Title;
				tbAuthor.Text = TheBook.Author;
				tbLanguage.Text = TheBook.Language;
				tbYearPublished.Text = TheBook.YearPublished.ToString();
				tbGenre.Text = TheBook.Genre;
				tbPublisher.Text = TheBook.Publisher;
				tbNumPages.Text = TheBook.NumPages.ToString();
				tbWordCount.Text = TheBook.WordCount.ToString();
			}


		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			TheBook.Title = tbName.Text.Trim();
			TheBook.Author = tbAuthor.Text.Trim();
			TheBook.Language = tbLanguage.Text.Trim();
			TheBook.YearPublished = int.Parse(tbYearPublished.Text.Trim());
			TheBook.Genre = tbGenre.Text.Trim();
			TheBook.Publisher = tbPublisher.Text.Trim();
			TheBook.NumPages = int.Parse(tbNumPages.Text.Trim());
			TheBook.WordCount = int.Parse(tbWordCount.Text.Trim());

			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		
	}
}
