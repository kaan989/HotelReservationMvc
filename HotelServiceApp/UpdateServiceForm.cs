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

namespace HotelServiceApp
{
    public partial class UpdateServiceForm : Form
    {
        private Service _serviceToUpdate;
        private string _connectionString;

        public UpdateServiceForm(Service service)
        {
            InitializeComponent();
            _serviceToUpdate = service;

           
            textBoxName.Text = _serviceToUpdate.Name;
            textBoxDescription.Text = _serviceToUpdate.Description;
            textBoxPrice.Text = _serviceToUpdate.Price.ToString();

            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            
            _serviceToUpdate.Name = textBoxName.Text;
            _serviceToUpdate.Description = textBoxDescription.Text;
            _serviceToUpdate.Price = decimal.Parse(textBoxPrice.Text);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
             
                context.Services.Update(_serviceToUpdate);
                context.SaveChanges(); 
                MessageBox.Show("Servis başarıyla güncellendi.");
            }

           
            this.Close();
        }
    }
}
