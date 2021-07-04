using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Base.ME = new Entities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Книги> booksFromBase = Base.ME.Книги.ToList();
            List<Book> books = new List<Book>();

            for(int i = 0; i < booksFromBase.Count; i++)
            {
                Book book = new Book();
                book.Название = booksFromBase[i].Название;
                book.autor = booksFromBase[i].Авторы.Автор;
                book.Количество_магазин = booksFromBase[i].Количество_магазин;
                book.Количество_склад = booksFromBase[i].Количество_склад;
                book.Цена = booksFromBase[i].Цена;
                book.Обложка = booksFromBase[i].Обложка;

                books.Add(book);
            }

            dataGrid_books.ItemsSource = books;
        }

        private void button_count_Click(object sender, RoutedEventArgs e)
        {
            Book book = (Book)dataGrid_books.SelectedItem;
            textBox_shopInfo.Text = Sumary.getSum(book).ToString() + " р.";
        }
    }
    public static class Sumary
    {
        static List<Book> books = new List<Book>();
        static public double getSum(Book book)
        {
            books.Add(book);

            double result = 0;
            for(int i = 0; i < books.Count; i++)
            {
                result += books[i].Цена;
            }
            double result2 = result;
            double sold = 0;
            while(result2 >= 500)
            {
                sold++;
                result2 -= 500;
            }

            if(books.Count >= 10)
            {
                return result * ((100 - (sold + 15))/100);
            }
            else if(books.Count >= 5)
            {
                return result * ((100 - (sold + 10)) / 100);
            }
            else if (books.Count >= 3)
            {
                return result * ((100 - (sold + 5)) / 100);
            }
            return result * (100-sold)/100;
        }
    }
    public class Book: Книги
    {

        public string autor;
    }
    class Base
    {
        public static Entities ME;
    }
}
