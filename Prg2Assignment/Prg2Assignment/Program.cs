using Prg2Assignment;
using System.Reflection.Metadata.Ecma335;

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

void ShowGuestDetails(List <Guest> guestList){
    Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20}", "Name", "PassportNumber", "CheckinDate", "CheckoutDate", "Membership Status", "Membership Points");
    for (int i = 0; i < guestList.Count; i++)
    {
        Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20}", 
            guestList[i].name, guestList[i].passportNum, DateOnly.FromDateTime(guestList[i].hotelStay.checkinDate), DateOnly.FromDateTime(guestList[i].hotelStay.checkoutDate), guestList[i].membership.status, guestList[i].membership.points);
    }
    Console.WriteLine("\n\n");
    
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
                standard.roomNumber, standard.bedConfiguration, standard.dailyRate, standard.isAvail, standard.requireWifi, standard.requireBreakfast, "NULL");
        }

        else if (roomList[i] is DeluxeRoom)
        {
            DeluxeRoom deluxe = (DeluxeRoom)roomList[i];
            Console.WriteLine("{0,-10} {1,-20} {2,-25} {3,-25} {4, -20} {5, -20} {6, -20}",
                deluxe.roomNumber, deluxe.bedConfiguration, deluxe.dailyRate, deluxe.isAvail, "NULL", "NULL", deluxe.additionalBed);
        }
    }
    Console.WriteLine("\n\n");
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
    try
    {
        Console.Write("Enter guest Passport Number: ");
        string CheckInPassNum = Console.ReadLine();
        for (int i = 0; i < guestList.Count;i++)
        {
            if(CheckInPassNum == "exit")
            {
                break;
            }
            else if (guestList[i].passportNum == CheckInPassNum)
            {
                try
                {
                    Console.Write("Enter check in date e.g.(DD/MM/YYYY): ");
                    DateTime CheckInDate = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Enter check out date e.g.(DD/MM/YYYY): ");
                    DateTime CheckOutDate = Convert.ToDateTime(Console.ReadLine());

                    Stay NewStay = new Stay(CheckInDate, CheckOutDate);         

                    ShowRoomDetails(roomList);
                    InputRoom(roomList, NewStay, guestList[i]);
                    ExtraRoom(roomList, NewStay, guestList[i]);
                    Console.WriteLine("Guest successfully checked in!");
                    Console.WriteLine("\n\n");     /////
                    ShowGuestDetails(guestList);  ///// To remove
                    ShowRoomDetails(roomList);   /////
                    return;

                }

                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input");
                    CheckInGuest(guestList, roomList);
                }
            }

            else
            {
                continue;
            }
          
        }
        Console.WriteLine("Guest not found");
        CheckInGuest(guestList, roomList);

    }
    catch (Exception ex)
    {
        Console.WriteLine("Invalid Input");
    }
}

void Main()
{
    InitGuestData(guestList);
    InitRoomData(roomList);
    while (true)
    {
        
        Console.WriteLine("[1] List all guests \n[2] List all rooms \n[3] Register guest \n[4] CheckIn guest \n[0] Exit Program");

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

