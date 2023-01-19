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

void InitRoomData(List<Room> roomList, List<Guest> guestList, List<Stay> stayList)
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
                StandardRoom standard = new StandardRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), true, true, true);
            }

            else if (data[0] == "Deluxe")
            {

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
            guestList[i].name, guestList[i].passportNum, guestList[i].hotelStay.checkinDate, guestList[i].hotelStay.checkoutDate, guestList[i].membership.status, guestList[i].membership.points);
    }
}

void ShowRoomDetails()
{
    using (StreamReader sr = new StreamReader("Rooms.csv"))
    {
        var lines = sr.ReadLine();
        if (lines != null)
        {
            string[] heading = lines.Split(',');
            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}",
                heading[0], heading[1], heading[2], heading[3]);
        }

        while ((lines = sr.ReadLine()) != null)
        {
            string[] data = lines.Split(',');

            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}",
                data[0], data[1], data[2], data[3]);
        }
    }
}

InitGuestData(guestList);
InitStayData(guestList, stayList);
ShowGuestDetails(guestList);
ShowRoomDetails();
