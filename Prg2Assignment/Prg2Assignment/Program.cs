using Prg2Assignment;

string GuestPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Guests.csv";
string RoomsPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Rooms.csv";
string StaysPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Stays.csv";

//Display all guest details//

List <Guest> guestList = new List<Guest>(); 
List <Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();


void InitStayData(List <Guest> guestList, List<Stay> stayList)
{
    using (StreamReader sr = new StreamReader("Stays.csv"))
    {
        var lines = sr.ReadLine();
        if (lines != null)
        {
            string[] heading = lines.Split(',');
        }
        while ((lines = sr.ReadLine()) != null)
        {
            string[] data = lines.Split(',');
            Stay stay = new Stay(Convert.ToDateTime(data[3]), Convert.ToDateTime(data[4]));
            
            
            for (int i = 0; i < guestList.Count; i++)
            {
                if (guestList[i].passportNum == data[1])
                {
                    guestList[i].hotelStay = stay;
                }
                else
                {
                    continue;
                }
            }
        }

    }
}


void InitRoomData(List<Room> roomList)
{
    using (StreamReader sr = new StreamReader("Rooms.csv"))
    {
        var lines = sr.ReadLine();
        if (lines != null)
        {
            string[] heading = lines.Split(',');
        }

        while ((lines = sr.ReadLine()) != null)
        {
            string[] data = lines.Split(',');

            if (data[0] == "Standard")
            {
                StandardRoom standard = new StandardRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), false, false, false);
                roomList.Add(standard);
            }

            else if (data[0] == "Deluxe")
            {
                DeluxeRoom deluxe = new DeluxeRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), false, false);
                roomList.Add(deluxe);
            }

            else
            {
                continue;
            }
        }
    }
}

void InitGuestData(List<Guest> guestList)
{
    
    using (StreamReader sr = new StreamReader("Guests.csv"))
    {
        var lines = sr.ReadLine();
        if (lines != null)
        {
            string[] heading = lines.Split(',');
        }

        while ((lines = sr.ReadLine()) != null)
        {
            string[] data = lines.Split(',');
            Membership membership = new Membership(data[2], Convert.ToInt32(data[3]));
            Guest guest = new Guest(data[0], data[1], null, new Membership(data[2], Convert.ToInt32(data[3])));
        
            guestList.Add(guest);
        }
    }
}

void ShowGuestDetails(List <Guest> guestList){
    Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20}", "Name", "PassportNumber", "CheckinDate", "CheckoutDate", "Membership Status", "Membership Points");
    for (int i = 0; i < guestList.Count; i++)
    {
        Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20}", 
            guestList[i].name, guestList[i].passportNum, null, null, guestList[i].membership.status, guestList[i].membership.points);
    }
    
}

void ShowRoomDetails(List <Room> roomList)
{
    Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20} {6, -20}", 
        "RoomNumber", "BedConfig", "DailyRate", "Availability", "RequireWifi", "RequireBreakfast", "AdditionalBed");

    for (int i = 0; i < roomList.Count; i++)
    {
        if (roomList[i] is StandardRoom)
        {
            StandardRoom standard = (StandardRoom)roomList[i];
            Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20} {6, -20}",
                standard.roomNumber, standard.bedConfiguration, standard.dailyRate, standard.isAvail, standard.requireWifi, standard.requireBreakfast, null);

        }

        else if (roomList[i] is DeluxeRoom)
        {
            DeluxeRoom deluxe = (DeluxeRoom)roomList[i];
            Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20} {6, -20}",
                deluxe.roomNumber, deluxe.bedConfiguration, deluxe.dailyRate, deluxe.isAvail, null, null, deluxe.additionalBed);
        }
    }
}


void Main()
{
    while (true)
    {
        InitGuestData(guestList);
        InitRoomData(roomList);
        Console.WriteLine("[1] List all guests \n[2] List all rooms \n[3] Register guest \n[0] Exit Program");

        try
        {
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowGuestDetails(guestList);
                    break;

                case "2":
                    ShowRoomDetails(roomList);
                    break;

                case "0":
                    Console.WriteLine("Exiting Program... ...");
                    return;
                default:
                    throw new NullReferenceException();

            }
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Invalid Option");
        }
    }
    
}

Main();



