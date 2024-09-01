using HotelReservationMvc.Data;
using HotelReservationMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HotelServiceApp
{
    public partial class Form1 : Form
    {
        private string _connectionString;
        public Form1()
        {
            InitializeComponent();
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }


        private void comboBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRooms.SelectedValue is int selectedRoomId)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(_connectionString);

                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    var selectedRoom = context.Rooms
                        .FirstOrDefault(r => r.Id == selectedRoomId);

                    // Handle the selected room here
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var rooms = context.Rooms
                     .Where(r => r.IsAvailable == false)
                     .ToList();

                comboBoxRooms.DataSource = rooms;
                comboBoxRooms.DisplayMember = "RoomNumber"; // Görüntülenecek alan
                comboBoxRooms.ValueMember = "Id"; // Seçilen deðer
            }
        }

        private void AddService_Click(object sender, EventArgs e)
        {
            int selectedRoomId = (int)comboBoxRooms.SelectedValue;
            string serviceName = textBoxName.Text;
            string description = textBoxDescription.Text;
            decimal price = decimal.Parse(textBoxPrice.Text);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                bool roomExists = context.Reservations.Any(r => r.RoomId == selectedRoomId);
                if (!roomExists)
                {
                    MessageBox.Show("Geçerli bir rezervasyon bulunamadý.");
                    return;
                }

                var reservation = context.Reservations
                    .FirstOrDefault(r => r.RoomId == selectedRoomId);

                var newService = new Service
                {
                    Name = serviceName,
                    Description = description,
                    Price = price
                };

                context.Services.Add(newService);
                context.SaveChanges(); // Servisi kaydedin

                var newReservationService = new ReservationService
                {
                    ReservationId = reservation.Id,
                    ServiceId = newService.Id,
                };

                context.ReservationServices.Add(newReservationService);
                context.SaveChanges(); // Rezervasyon servisini kaydedin

                textBoxName.Clear();
                textBoxDescription.Clear();
                textBoxPrice.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormServices formServices = new FormServices();
            formServices.ShowDialog();
        }
    }
}



