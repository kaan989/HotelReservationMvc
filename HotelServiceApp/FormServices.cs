using HotelReservationMvc.Data;
using HotelReservationMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelServiceApp
{
    public partial class FormServices : Form
    {
        private string _connectionString;
        public FormServices()
        {
            InitializeComponent();
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private void FormServices_Load(object sender, EventArgs e)
        {

        }

        private void listbutton_Click(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                List<Service> services = context.Services.ToList();
                listBox1.Items.Clear(); // Önceki öğeleri temizle

                foreach (var service in services)
                {
                    // Service nesnesini doğrudan ListBox'a ekleyin
                    listBox1.Items.Add(service);
                }

                // Görüntülenecek olan alanı belirtin (örneğin, sadece servisin adını gösterin)
                listBox1.DisplayMember = "Description";
               
            }
        }

        private void DeleteService_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // Seçilen öğeyi Service nesnesine cast edin
                Service selectedService = (Service)listBox1.SelectedItem;

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(_connectionString);

                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    var serviceToDelete = context.Services.FirstOrDefault(s => s.Id == selectedService.Id);

                    if (serviceToDelete != null)
                    {
                        context.Services.Remove(serviceToDelete);
                        context.SaveChanges(); // Silme işlemi kaydedilir

                        MessageBox.Show("Servis başarıyla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Servis bulunamadı.");
                    }
                }

                // ListBox'ı güncelle
                listbutton_Click(null, null);
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir servis seçin.");
            }

        }

        private void UpdateServiceButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // Seçilen öğeyi Service nesnesine cast edin
                Service selectedService = (Service)listBox1.SelectedItem;

                // UpdateServiceForm formunu açın ve servisi geçirin
                UpdateServiceForm updateForm = new UpdateServiceForm(selectedService);
                updateForm.ShowDialog();

                // Form kapandıktan sonra ListBox'ı güncelle
                listbutton_Click(null, null);
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir servis seçin.");
            }
        }
    }
}
