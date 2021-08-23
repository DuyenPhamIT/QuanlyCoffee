using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitaplon
{
    public partial class Manager : Form
    {
        BaitapDataContext data = new BaitapDataContext();

        public Manager()
        {
            InitializeComponent();
        }

        public int GetOrder(){
            var list = data.Orders.FirstOrDefault(o => o.Status == 0);

            if (list != null)
            {
                return list.OrderId;
            }

            return -1;
        }

        public void insertOrderDetail(int idOrder,int idProduct,int quantity,string size)
        {
            var isExistOrderDetail = data.OrderDetails.FirstOrDefault(o => o.OrderId == idOrder && o.ProductId == idProduct && o.Size == size);
            var countProductOld = data.OrderDetails.FirstOrDefault(o => o.OrderId == idOrder && o.ProductId == idProduct && o.Size == size);

            if (isExistOrderDetail != null)
            {
                int countProductNew = countProductOld.Quantity.Value + int.Parse(nmFoodCount.Value.ToString());

                if (countProductNew > 0)
                {
                    var od1 = data.OrderDetails.Single(o => o.OrderId == idOrder && o.ProductId == idProduct && o.Size == size);
                    od1.Quantity = countProductNew;

                    data.SubmitChanges();
                }
                else
                {
                    OrderDetail od = data.OrderDetails.Single(o => o.OrderId == idOrder && o.ProductId == idProduct && o.Size == size);
                    data.OrderDetails.DeleteOnSubmit(od);
                    data.SubmitChanges();
                }
            }
            else
            {
                if (quantity <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ!");
                    return;
                }
                OrderDetail ord = new OrderDetail();
                ord.OrderId = idOrder;
                ord.ProductId = idProduct;
                ord.Size = size;
                ord.Quantity = quantity;
                data.OrderDetails.InsertOnSubmit(ord);
                data.SubmitChanges();
            }
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (cboSize.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn size!!!");
                return;
            }

            int orderID = GetOrder();
            int idPro = int.Parse(cbFood.SelectedValue.ToString());
            int quantity = int.Parse(nmFoodCount.Value.ToString());
            string size = cboSize.SelectedItem.ToString();
            
            if (orderID != -1)
            {
                insertOrderDetail(orderID,idPro,quantity,size);
                
                LoadOrderDetail(orderID);
            }
            else
            {

                Order od = new Order();
                od.startDate = DateTime.Now;
                od.enddate = null;
                od.Status = 0;

                data.Orders.InsertOnSubmit(od);
                data.SubmitChanges();

                var idOrderMax = data.Orders.Max(o => o.OrderId);

                insertOrderDetail(idOrderMax, idPro, quantity, size);

                LoadOrderDetail(idOrderMax);
            }


            txtDis.Text = null;
            
        }


        private void LoadOrderDetail(int OrderID)
        {

            var dataOrderDetail = data.USP_GetListOrder(OrderID);

            dgvBill.DataSource = dataOrderDetail.ToList();
            LoadTotalPrice();
        }

        void LoadTotalPrice()
        {
            int sc = dgvBill.Rows.Count;
            float thanhtien = 0;
            for (int i = 0; i < sc; i++)
                thanhtien += float.Parse(dgvBill.Rows[i].Cells["TotalPrice"].Value.ToString());
            txbTotalPrice.Text = thanhtien.ToString();
        }
        private void LoadFood()
        {
            var dataCat = from Cat in data.Categories
                          select new
                          {
                              CategoryId = Cat.CategoryId,
                              CategoryName = Cat.CategoryName

                          };
            cbCategory.DataSource = dataCat;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryId";
        }
        private void LoadProduct()
        {
            var dataPro = from Pro in data.Products
                          join c in data.Categories on Pro.CategoryId equals c.CategoryId
                          select new
                          {
                              ProductId = Pro.ProductId,
                              CategoryName = c.CategoryName,
                              ProductName = Pro.ProductName,
                              Price = Pro.Price,
                          };
            cbFood.DataSource = dataPro.ToList();
            cbFood.DisplayMember = "ProductName";
            cbFood.ValueMember = "ProductId";
        }
        
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            int discount = 0;
            int OrderID = GetOrder();
            if (txtDis.Text != "")
            {
                discount = Convert.ToInt32(txtDis.Text);
            }
            
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text);
            double finaltotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (OrderID != -1)
            {
                /*string HoaDon = "";
                HoaDon += dgvBill;
                HoaDon += "\nTổng cộng: " + finaltotalPrice + " VNĐ\n";
                e.Graphics.DrawString(HoaDon, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 100, 200);*/
                DialogResult ms = MessageBox.Show("Bạn có muốn thanh toán "+"\n" + dgvBill + "\nTổng tiền: " + finaltotalPrice + " VNĐ", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                if (ms == DialogResult.Yes)
                {
                    MessageBox.Show("Đã thanh toán " + finaltotalPrice, "Xong", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var order = data.Orders.Single(o => o.OrderId == OrderID);
                    order.Status = 1;
                    order.enddate = DateTime.Now;
                    order.Discount = discount;
                    order.Price = finaltotalPrice;
                    //data.Orders.InsertOnSubmit(order);
                    data.SubmitChanges();
                    LoadOrderDetail(OrderID);
                }
                
            }
            
        }


        private void txbTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void Manager_Load(object sender, EventArgs e)
        {
            LoadFood();
            LoadProduct();
        }

        private void dgvBill_Click(object sender, EventArgs e)
        {
            /*string id = this.dgvBill.CurrentRow.Cells[0].Value.ToString();
            OrderDetail ord = data.OrderDetails.Single(p => p.STT.Equals(id));
            cbFood.SelectedValue = ord.ProductId;
            cboSize.SelectedItem = ord.Size;
            nmFoodCount.Value = (decimal)ord.Quantity;*/
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            /* int id = 0;
            ComboBox cbb = sender as ComboBox;

            if (cbb.SelectedIndex == null)
            {
                return;
            }

            Category cate = cbb.SelectedItem as Category;
            id = cate.CategoryId;

            LoadProduct(id);
            */
        }
    }
}
