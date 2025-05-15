public class Guest
{
    public int Id { get; set; }
    
    [Required, MaxLength(200)]
    public string FirstName { get; set; }

    [Required, MaxLength(400)]
    public string LastName { get; set; }

    [Required]
    public DateTime DOB { get; set; }

    [Required, MaxLength(600)]
    public string Address { get; set; }

    [Required]
    public string Nationality { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Required]
    public int RoomId { get; set; }

    public Room Room { get; set; }
}
