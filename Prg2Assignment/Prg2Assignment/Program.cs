using Prg2Assignment;
using System.Reflection.Metadata.Ecma335;

string GuestPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Guests.csv";
string RoomsPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Rooms.csv";
string StaysPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Stays.csv";


List <Guest> guestList = new List<Guest>(); 
List <Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();
string border = new string('-', 10);

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
                StandardRoom standard = new StandardRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), true, default, default);
                roomList.Add(standard);
            }

            else if (data[0] == "Deluxe")
            {
                DeluxeRoom deluxe = new DeluxeRoom(Convert.ToInt32(data[1]), data[2], Convert.ToDouble(data[3]), true, default);
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
            Stay stay = new Stay(default, default);
            Guest guest = new Guest(data[0], data[1], stay, membership); //Change Null to Stay object
            guestList.Add(guest);
        }
    }
}



void ShowGuestDetails(List<Guest> guestlist)
{
    Console.WriteLine(border + "Guest details" + border);
    foreach(Guest guest in guestList)
    {
        Console.WriteLine(guest.ToString());
    }
    Console.WriteLine(border + border + border);
    Console.WriteLine("\n\n");
}


void ShowRoomDetails(List<Room> roomList)
{
    Console.WriteLine(border + "Room Details" + border);
    foreach (Room room in roomList)
    {
        Console.WriteLine(room.ToString());
        Console.WriteLine("\n");
    }
    Console.WriteLine(border + border + border);
}

void ShowStayDetails(List<Guest> guestList)
{
    ShowGuestDetails(guestList);

    try
    {
        Console.Write("Enter guest Passport Number: ");
        string PassNum = Console.ReadLine();

        for (int i = 0; i < guestList.Count; i++)
        {
            if (PassNum == "exit")
            {
                break;
            }
            else if (guestList[i].passportNum == PassNum)
            {
                if (guestList[i].hotelStay.checkinDate == default)
                {
                    Console.WriteLine("Guest has not checked in");
                }

                else
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20} {5,-25} {6,-25} {7, -20} {8, -20} {9, -20}",
                        "Name", "CheckInDate", "CheckOutDate", "RoomNumber", "BedConfig", "DailyRate", "Availability", "RequireWifi", "RequireBreakfast", "AdditionalBed");

                    for (int x = 0; x < guestList[i].hotelStay.roomlist.Count; x++)
                    {
                        if (guestList[i].hotelStay.roomlist[x] is StandardRoom)
                        {
                            Guest guest = guestList[i];
                            Stay stay = guestList[i].hotelStay;
                            StandardRoom standard = (StandardRoom)guestList[i].hotelStay.roomlist[x];

                            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20} {5,-25} {6,-25} {7, -20} {8, -20} {9, -20}",
                                guest.name, DateOnly.FromDateTime(stay.checkinDate), DateOnly.FromDateTime(stay.checkoutDate), standard.roomNumber, standard.bedConfiguration, standard.dailyRate, standard.isAvail, standard.requireWifi, standard.requireBreakfast, "NULL");
                        }

                        else if (guestList[i].hotelStay.roomlist[x] is DeluxeRoom)
                        {
                            Guest guest = guestList[i];
                            Stay stay = guestList[i].hotelStay;
                            DeluxeRoom deluxe = (DeluxeRoom)guestList[i].hotelStay.roomlist[x];

                            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20} {5,-25} {6,-25} {7, -20} {8, -20} {9, -20}",
                                guest.name, DateOnly.FromDateTime(stay.checkinDate), DateOnly.FromDateTime(stay.checkoutDate), deluxe.roomNumber, deluxe.bedConfiguration, deluxe.dailyRate, deluxe.isAvail, "NULL", "NULL", deluxe.additionalBed);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
              
            }

            else
            {
                continue;
            }
        }
    }

    catch (Exception ex)
    {
        Console.WriteLine("Invalid input");
    }
}

void RegisterGuest(List <Guest> guestList)
{
    ShowGuestDetails(guestList);
    try
    {
        Console.Write("Enter Name: ");
        string Name = Console.ReadLine();

        Console.Write("Enter Passport Number: ");
        string PassNum = Console.ReadLine();
        if (PassNum.Length == 9) //checks if passport num length is valid
        {
            for (int i = 0; i < guestList.Count; i++) //checks if passport num already exists
            {
                if (guestList[i].passportNum == PassNum)
                {
                    Console.WriteLine("Passport Number already exists");
                    RegisterGuest(guestList);
                }

                else
                {
                    continue;
                }
            }
            Membership membership = new Membership("Ordinary", 0);
            Guest NewGuest = new Guest(Name, PassNum, null, membership);
            guestList.Add(NewGuest);
            using (StreamWriter sw = new StreamWriter("Guests.csv", true))
            {
                sw.WriteLine("{0},{1},{2},{3}", Name, PassNum, membership.status, membership.points);
            }
            ShowGuestDetails(guestList);

        }
        else
        {
            Console.WriteLine("Invalid Passport Number");
            RegisterGuest(guestList);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Invalid Input");
    }
}

void ExtraRoom(List<Room> roomList, Stay NewStay, Guest NewGuest)
{
    Console.Write("Do you wish to select another room [Y/N]: ");
    string AddOption = (Console.ReadLine()).ToUpper();
    if (AddOption == "exit")
    {
        return;
    }

    else if (AddOption == "Y")
    {
        InputRoom(roomList, NewStay, NewGuest);
    }

    else if (AddOption == "N")
    {
        return;
    }

    else
    {
        return;
    }
}

void InputRoom(List<Room>roomList, Stay NewStay, Guest NewGuest)
{
    ShowRoomDetails(roomList);
    try
    {
        Console.Write("Enter room number: ");
        int RoomNum = Convert.ToInt32(Console.ReadLine());

        for (int x = 0; x < roomList.Count; x++)
        {

            if (roomList[x].roomNumber == RoomNum)
            {
                if (roomList[x] is StandardRoom)
                {
                    StandardRoom NewStandard = (StandardRoom)roomList[x];

                    Console.Write("Require Wifi [Y/N]: ");
                    string WifiOption = Console.ReadLine();
                    if (WifiOption.ToUpper() == "Y")
                    {
                        NewStandard.requireWifi = true;
                    }

                    else if (WifiOption.ToUpper() == "N")
                    {
                        NewStandard.requireWifi = false;
                    }

                    else
                    {
                        Console.WriteLine("Invalid input");
                        CheckInGuest(guestList, roomList);

                    }

                    Console.Write("Require Breakfast [Y/N]: ");
                    string BFOption = Console.ReadLine();
                    if (BFOption.ToUpper() == "Y")
                    {
                        NewStandard.requireBreakfast = true;
                        NewStandard.isAvail = false;
                    }

                    else if (BFOption.ToUpper() == "N")
                    {
                        NewStandard.requireBreakfast = false;
                        NewStandard.isAvail = false;
                    }

                    else
                    {
                        Console.WriteLine("Invalid input");
                        CheckInGuest(guestList, roomList);
                    }
                    NewStay.roomlist.Add(NewStandard);
                    NewGuest.hotelStay = NewStay;
                }

                else if (roomList[x] is DeluxeRoom)
                {
                    DeluxeRoom NewDeluxe = (DeluxeRoom)roomList[x];
                    Console.Write("Additional Bed [Y/N]: ");
                    string ABOption = Console.ReadLine();
                    if (ABOption.ToUpper() == "Y")
                    {
                        NewDeluxe.additionalBed = true;
                        NewDeluxe.isAvail = false;
                    }

                    else if (ABOption.ToUpper() == "N")
                    {
                        NewDeluxe.additionalBed = false;
                        NewDeluxe.isAvail = false;
                    }

                    else
                    {
                        Console.WriteLine("Invalid input");
                        CheckInGuest(guestList, roomList);
                    }
                    NewStay.roomlist.Add(NewDeluxe);
                    NewGuest.hotelStay = NewStay;
                }
                /* Console.Write("Do you wish to select another room? [Y/N] ");
                string AnotherOption = (Console.ReadLine()).ToUpper();
                if (AnotherOption == "Y")
                {

                } */
                return;
            }
        }
        Console.WriteLine("Room not found");
        CheckInGuest(guestList, roomList);
    }

    catch (Exception ex2)
    {
        Console.WriteLine("Invalid Input");
        CheckInGuest(guestList, roomList);
    }
}

void CheckInGuest(List <Guest> guestList, List <Room> roomList)
{
    ShowGuestDetails(guestList);

    Guest NewGuest = SearchGuest(guestList);
    try
    {
        Console.Write("Enter check in date e.g.(DD/MM/YYYY): ");
        DateTime CheckInDate = Convert.ToDateTime(Console.ReadLine());

        Console.Write("Enter check out date e.g.(DD/MM/YYYY): ");
        DateTime CheckOutDate = Convert.ToDateTime(Console.ReadLine());

        Stay NewStay = new Stay(CheckInDate, CheckOutDate);


        InputRoom(roomList, NewStay, NewGuest);
        ExtraRoom(roomList, NewStay, NewGuest);
        NewGuest.iSCheckedin = true;

        Console.WriteLine("\n");
        Console.WriteLine("#################################");
        Console.WriteLine("\n");
        Console.WriteLine("Guest successfully checked in!");
        Console.WriteLine("\n");
        Console.WriteLine("#################################");
        Console.WriteLine("\n\n");

        ShowGuestDetails(guestList);
        ShowRoomDetails(roomList);
        return;

    }

    catch (Exception ex)
    {
        Console.WriteLine("Invalid Input");
        CheckInGuest(guestList, roomList);
    }

}


void CheckOutGuest(List<Guest> guestList, List<Room> roomList)
{
    ShowGuestDetails(guestList);
    Guest CheckGuest = SearchGuest(guestList);
    try
    {
        CheckGuest.iSCheckedin = false;
        foreach (Room CheckedRoom in CheckGuest.hotelStay.roomlist)
        {
            CheckedRoom.isAvail= true;
        }
        guestList.Remove(CheckGuest);
        Console.WriteLine("\n");
        Console.WriteLine("#################################");
        Console.WriteLine("\n");
        Console.WriteLine("Guest successfully checked out!");
        Console.WriteLine("\n");
        Console.WriteLine("#################################");
        Console.WriteLine("\n\n");
        ShowGuestDetails(guestList);
    }
    
    catch (Exception ex)
    {
        Console.WriteLine("Invalid Input");
        CheckOutGuest(guestList, roomList);
    }
}
Guest SearchGuest(List<Guest> guestList)
{
    Console.WriteLine("Enter passport number: ");
    string PassNum = Console.ReadLine();
    

    for (int i = 0; i < guestList.Count; i++)
    {
        if ("S11223344B" == PassNum)
        {
            Console.WriteLine(guestList[i].passportNum);
            return guestList[i];
        }

        else
        {
            continue;
        }
    }
    return null;
}


void ExtendStay(List<Guest> guestlist)
{
    ShowGuestDetails(guestlist);
    Console.Write("Enter passport number: ");
    string UserInput = Console.ReadLine();
    for (int i = 0; i < guestList.Count; i++)
    {
        if (guestlist[i].iSCheckedin == false && guestlist[i].passportNum == UserInput)
        {
            Console.WriteLine("Guest not checked in");
        }

        else if (guestlist[i].iSCheckedin == true && guestlist[i].passportNum == UserInput)
        {
            Console.Write("Enter number of days to extend: ");
            int Extend = Convert.ToInt32(Console.ReadLine());
            guestlist[i].hotelStay.checkoutDate = guestlist[i].hotelStay.checkoutDate.AddDays(Extend);
            Console.WriteLine("\n");
            Console.WriteLine("#################################");
            Console.WriteLine("\n");
            Console.WriteLine("Stay successfully extended");
            Console.WriteLine("\n");
            Console.WriteLine("#################################");
            Console.WriteLine("\n\n");
            return;
        }
        else
        {
            continue;

        }


    }
}

void Main()
{
    InitGuestData(guestList);
    InitRoomData(roomList);
    while (true)
    {
        Console.WriteLine("[1] List all guests \n[2] List all rooms \n[3] Register guest \n[4] CheckIn guest \n[5] List stay details \n[6] Extend Stay \n[7] Check Out Guest \n[0] Exit Program");

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

                case "3":
                    RegisterGuest(guestList);
                    break;

                case "4":
                    CheckInGuest(guestList, roomList);
                    break;

                case "5":
                    ShowStayDetails(guestList);
                    break;

                case "6":
                    ExtendStay(guestList);
                    break;

                case "7":
                    CheckOutGuest(guestList, roomList);
                    break;

                case "0":
                    Console.WriteLine("Exiting Program... ...");
                    return;
                default: throw new Exception();
                

            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid Option");
        }
    }
    
}

Main();

