using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Form1 : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;

        private ArrayList ListOfProductCategory;
        private BindingSource showProductList;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArrayList pro = new ArrayList();
            pro.Add("Beverages");
            pro.Add("Bread/Bakery");
            pro.Add("Canned/Jarred Goods");
            pro.Add("Dairy");
            pro.Add("Frozen Goods");
            pro.Add("Meat");
            pro.Add("Personal Care");
            pro.Add("Other");

            foreach (string _Category in pro)
            {
                cbCategory.Items.Add(_Category);
            }

            showProductList = new BindingSource();

        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Error Product Name Input");
            }
                //Exception here
                return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                throw new NumberFormatException("Error Quantity Input");
            }
                //Exception here
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Error Price Input");
            }
                //Exception here
                return Convert.ToDouble(price);
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description)); gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; gridViewProductList.DataSource = showProductList;

            }
            catch (CurrencyFormatException em)
            {
                MessageBox.Show(em.Message);
            }
            catch (NumberFormatException em)
            {
                MessageBox.Show(em.Message);
            }
            catch (StringFormatException em)
            {
                MessageBox.Show(em.Message);
            }
        }
    }


    class NumberFormatException : Exception
    {
        public NumberFormatException(string qty): base(qty)
        {
        }
    }
    class StringFormatException : Exception
    {
        public StringFormatException(string name) : base(name)
        {
        }
    }
    class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string price) : base(price)
        {
        }
    }
}
