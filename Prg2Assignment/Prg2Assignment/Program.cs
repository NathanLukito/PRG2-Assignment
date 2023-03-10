//==========================================================
// Student Number : S10244400B
// Student Name : Nathan Farrel Lukito
//
// Student Number: S10243254B
// Student Name : Ervin Wong Yong Qi
//==========================================================




using Prg2Assignment;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml.Linq;

string GuestPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Guests.csv";
string RoomsPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Rooms.csv";
string StaysPath = "https://github.com/NathanLukito/PRG2-Assignment/blob/1aa849a8de2b114df39984bad9635403d24ab44b/Prg2Assignment/Prg2Assignment/bin/Debug/net6.0/Stays.csv";


List <Guest> guestList = new List<Guest>(); 
List <Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();
IDictionary<string, double> monthlyCharges = new Dictionary<string, double>()
{
    {"Jan", 0.00},
    {"Feb", 0.00},
    {"Mar", 0.00},
    {"Apr", 0.00},
    {"May", 0.00},
    {"Jun", 0.00},
    {"Jul", 0.00},
    {"Aug", 0.00},
    {"Sep", 0.00},
    {"Oct", 0.00},
    {"Nov", 0.00},
    {"Dec", 0.00},
};

string border = new string('-', 10);

void ValidatePassport(string str)
{
    Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //input must be between a to Z upper and lowercase and can be integer
    if(str.Length == 0)
    {
        throw new ArgumentNullException();
    }
    else if(str.Length != 9)
    {
        throw new ArgumentOutOfRangeException();
    }
    else if(regex.IsMatch(str) == true)
    {
        
    }
    else
    {
        throw new ArgumentException();
    }
}

void ValidateName(string name)
{
    Regex regex = new Regex(@"^[a-zA-Z]+$"); //input must be between a to Z either upper or lowercase
    if(name.Length == 0)
    {
        throw new ArgumentNullException();
    }
    else if (regex.IsMatch(name) == true)
    {
        //Do Nothing
    }
    else
    {
        throw new ArgumentException(); 
    }
    
}


void ValidateYear(string year)
{
    Regex regex = new Regex(@"^[0-9]+$"); //Input must be between 0-9
    if (year.Length == 0)
    {
        throw new ArgumentNullException();
    }
    else if (regex.IsMatch(year) == true)
    {
        //Does nothing
    }

    else
    {
        throw new ArgumentException();
    }
}

void InitStayData(List <Guest> guestList, List<Room> roomList)
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
            OverrideRoom(stay);
            OverrideStay(guestList, stay);

            void OverrideRoom(Stay stay)
            {
                foreach (Room room in roomList)
                {
                    if (room.roomNumber == Convert.ToInt32(data[5]))
                    {
                        if (room is DeluxeRoom)
                        {
                            DeluxeRoom deluxe = (DeluxeRoom)room;
                            deluxe.additionalBed = Convert.ToBoolean(data[8]);
                            if (data[8] == "TRUE")
                            {
                                deluxe.dailyRate += 25;
                                deluxe.isAvail = false;
                                stay.AddRoom(deluxe);
                                CheckExtraRoom(stay);
                            }
                            else
                            {
                                deluxe.isAvail = false;
                                stay.AddRoom(deluxe);
                                CheckExtraRoom(stay);
                                continue;
                            }
                        }

                        else if (room is StandardRoom)
                        {
                            StandardRoom standard = (StandardRoom)room;
                            standard.requireWifi = Convert.ToBoolean(data[6]);
                            standard.requireBreakfast = Convert.ToBoolean(data[7]);
                            if (data[6] == "TRUE")
                            {
                                standard.dailyRate += 10;
                                if (data[7] == "TRUE")
                                {
                                    standard.dailyRate += 20;
                                    standard.isAvail = false;
                                    stay.AddRoom(standard);
                                    CheckExtraRoom(stay);
                                }
                                else
                                {
                                    standard.isAvail = false;
                                    stay.AddRoom(standard);
                                    CheckExtraRoom(stay);
                                }
                            }
                            else
                            {
                                if (data[7] == "TRUE")
                                {
                                    standard.dailyRate += 20;
                                    standard.isAvail = false;
                                    stay.AddRoom(standard);
                                    CheckExtraRoom(stay);
                                }
                                else
                                {
                                    standard.isAvail = false;
                                    stay.AddRoom(standard);
                                    CheckExtraRoom(stay);
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

            void OverrideStay(List<Guest> guestList, Stay stay)
            {
                for (int i = 0; i < guestList.Count; i++)
                {
                    if (guestList[i].passportNum == data[1])
                    {
                        guestList[i].hotelStay = stay;
                        guestList[i].iSCheckedin = Convert.ToBoolean(data[2]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            void CheckExtraRoom(Stay stay)
            {
                foreach (Room room in roomList)
                {
                    if (data[9] != "")
                    {
                        if (room.roomNumber == Convert.ToInt32(data[9]) )
                        {
                            if (room is StandardRoom)
                            {
                                StandardRoom standard = (StandardRoom)room;
                                standard.requireWifi = Convert.ToBoolean(data[10]);
                                standard.requireBreakfast = Convert.ToBoolean(data[11]);
                                if (data[10] == "TRUE")
                                {
                                    standard.dailyRate += 10;
                                    if (data[11] == "TRUE")
                                    {
                                        standard.isAvail = false;
                                        standard.dailyRate += 20;
                                        stay.AddRoom(standard);
                                    }
                                    else
                                    {
                                        standard.isAvail = false;
                                        stay.AddRoom(standard);
                                    }
                                }
                                else
                                {
                                    if (data[11] == "TRUE")
                                    {
                                        standard.isAvail = false;
                                        standard.dailyRate += 20;
                                        stay.AddRoom(standard);
                                    }
                                    else
                                    {
                                        standard.isAvail = false;
                                        stay.AddRoom(standard);
                                    }
                                }
                                
                                
                            }

                            else
                            {
                                DeluxeRoom deluxe = (DeluxeRoom)room;
                                deluxe.isAvail = false;
                                deluxe.additionalBed = false;
                                stay.AddRoom(deluxe);
                            }
                        }

                        else
                        {
                            continue;
                        }
                    }

                    else
                    {
                        continue;
                    }

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
            try
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
            catch (FormatException)
            {
                Console.WriteLine("Data added to room object is in a invalid format");
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
            try
            {
                string[] data = lines.Split(',');
                Membership membership = new Membership(data[2], Convert.ToInt32(data[3]));
                Stay stay = new Stay(default, default);
                Guest guest = new Guest(data[0], data[1], stay, membership); //Change Null to Stay object
                guestList.Add(guest);
            }
            catch (FormatException)
            {
                Console.WriteLine("Data added to guest object is in a invalid format");
            }
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

//Show available rooms//
void ShowRoomDetails(List<Room> roomList) 
{
    Console.WriteLine(border + "Room Details" + border);
    foreach (Room room in roomList)
    {
        if (room.isAvail == true)
        {
            Console.WriteLine(room.ToString());
            Console.WriteLine("\n");
        }

        else
        {
            continue;
        }
    }
    Console.WriteLine(border + border + border);
}

void ShowStayDetails(List<Guest> guestList)
{
    ShowGuestDetails(guestList);
    Guest guest = SearchGuest(guestList);
    Console.WriteLine("\n\n");
        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20} {5,-25} {6,-25} {7, -20} {8, -20} {9, -20}",
            "Name", "CheckInDate", "CheckOutDate", "RoomNumber", "BedConfig", "DailyRate", "Availability", "RequireWifi", "RequireBreakfast", "AdditionalBed");

        for (int x = 0; x < guest.hotelStay.roomlist.Count; x++)
        {
            if (guest.hotelStay.roomlist[x] is StandardRoom)
            {
                Stay stay = guest.hotelStay;
                StandardRoom standard = (StandardRoom)guest.hotelStay.roomlist[x];

                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20} {5,-25} {6,-25} {7, -20} {8, -20} {9, -20}",
                    guest.name, DateOnly.FromDateTime(stay.checkinDate), DateOnly.FromDateTime(stay.checkoutDate), standard.roomNumber, standard.bedConfiguration, standard.dailyRate, standard.isAvail, standard.requireWifi, standard.requireBreakfast, "NULL");
            }

            else if (guest.hotelStay.roomlist[x] is DeluxeRoom)
            {
                Stay stay = guest.hotelStay;
                DeluxeRoom deluxe = (DeluxeRoom)guest.hotelStay.roomlist[x];

                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20} {5,-25} {6,-25} {7, -20} {8, -20} {9, -20}",
                    guest.name, DateOnly.FromDateTime(stay.checkinDate), DateOnly.FromDateTime(stay.checkoutDate), deluxe.roomNumber, deluxe.bedConfiguration, deluxe.dailyRate, deluxe.isAvail, "NULL", "NULL", deluxe.additionalBed);
            }

            else
            {
                continue;
            }
        }
        
    
}

void RegisterGuest(List <Guest> guestList)
{
    ShowGuestDetails(guestList);
    
    Console.Write("Enter Name: ");
    string Name = Console.ReadLine();
    ValidateName(Name);
    Console.Write("Enter Passport Number: ");
    string PassNum = Console.ReadLine();
    ValidatePassport(PassNum);
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
    Stay stay = new Stay(default, default);
    Guest NewGuest = new Guest(Name, PassNum, stay, membership);
    guestList.Add(NewGuest);
    using (StreamWriter sw = new StreamWriter("Guests.csv", true))
    {
        sw.Write("{0},{1},{2},{3}", Name, PassNum, membership.status, membership.points);
    }
    ShowGuestDetails(guestList);

}

void ExtraRoom(List<Room> roomList, Stay NewStay, Guest NewGuest)
{
    try
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
            Console.WriteLine("Please enter or [Y/N]");
            ExtraRoom(roomList, NewStay, NewGuest);
        }
    }
    catch(ArgumentNullException)
    {
        Console.WriteLine("Cannot enter empty input");
        ExtraRoom(roomList, NewStay, NewGuest);
    }
    catch(FormatException)
    {
        Console.WriteLine("Please enter or [Y/N]");
        ExtraRoom(roomList, NewStay, NewGuest);
    }
    
}

void InputRoom(List<Room> roomList, Stay NewStay, Guest NewGuest)
{
    ShowRoomDetails(roomList);
    try
    {
        Console.Write("Enter room number: ");
        int RoomNum = Convert.ToInt32(Console.ReadLine());

        for (int x = 0; x < roomList.Count; x++)
        {

            if (roomList[x].roomNumber == RoomNum && roomList[x].isAvail == true)
            {
                if (roomList[x] is StandardRoom)
                {
                    StandardRoom NewStandard = (StandardRoom)roomList[x];
                    Console.Write("Require Wifi [Y/N]: ");
                    string WifiOption = Console.ReadLine();
                    if (WifiOption.ToUpper() == "Y")
                    {
                        NewStandard.requireWifi = true;
                        NewStandard.dailyRate += 10;
                        Console.Write("Require Breakfast [Y/N]: ");
                        string BFOption = Console.ReadLine();
                        if (BFOption.ToUpper() == "Y")
                        {
                            NewStandard.requireBreakfast = true;
                            NewStandard.isAvail = false;
                            NewStandard.dailyRate += 20;
                        }

                        else if (BFOption.ToUpper() == "N")
                        {
                            NewStandard.requireBreakfast = false;
                            NewStandard.isAvail = false;
                        }

                        else
                        {
                            Console.WriteLine("Input needs to be [Y/N]");
                            InputRoom(roomList, NewStay, NewGuest);
                        }
                        NewStay.AddRoom(NewStandard);
                        NewGuest.hotelStay = NewStay;
                    }

                    else if (WifiOption.ToUpper() == "N")
                    {
                        NewStandard.requireWifi = false;
                        Console.Write("Require Breakfast [Y/N]: ");
                        string BFOption = Console.ReadLine();
                        if (BFOption.ToUpper() == "Y")
                        {
                            NewStandard.requireBreakfast = true;
                            NewStandard.isAvail = false;
                            NewStandard.dailyRate += 20;
                        }

                        else if (BFOption.ToUpper() == "N")
                        {
                            NewStandard.requireBreakfast = false;
                            NewStandard.isAvail = false;
                        }

                        else
                        {
                            Console.WriteLine("Input needs to be [Y/N]");
                            InputRoom(roomList, NewStay, NewGuest);
                        }
                        NewStay.AddRoom(NewStandard);
                        NewGuest.hotelStay = NewStay;
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be [Y/N]");
                        InputRoom(roomList, NewStay, NewGuest);

                    }

                    
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
                        NewDeluxe.dailyRate += 25;
                    }

                    else if (ABOption.ToUpper() == "N")
                    {
                        NewDeluxe.additionalBed = false;
                        NewDeluxe.isAvail = false;
                    }

                    else
                    {
                        Console.WriteLine("Input needs to be [Y/N]");
                        InputRoom(roomList, NewStay, NewGuest);
                    }
                    NewStay.AddRoom(NewDeluxe);
                    NewGuest.hotelStay = NewStay;
                }

                return;
            }
            else if (roomList[x].roomNumber == RoomNum && roomList[x].isAvail == false)
            {
                Console.WriteLine("Room is unavailable");
                InputRoom(roomList, NewStay, NewGuest);
            }
            else
            {
                continue;
            }
        }
        Console.WriteLine("Room not found");
        InputRoom(roomList, NewStay, NewGuest);
    }
    
    catch(FormatException)
    {
        Console.WriteLine("Input needs to be a number");
        InputRoom(roomList, NewStay, NewGuest);
    }
    catch(OverflowException)
    {
        Console.WriteLine("Room numbers can only be in a 3 digit format");
        InputRoom(roomList, NewStay, NewGuest);
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
        if ((CheckInDate - DateTime.Now).Days < 0)
        {
            Console.WriteLine("Date entered cannot be in the past");
            CheckInGuest(guestList, roomList);
        }
        else
        {
            Console.Write("Enter check out date e.g.(DD/MM/YYYY): ");
            DateTime CheckOutDate = Convert.ToDateTime(Console.ReadLine());
            if ((CheckOutDate - CheckInDate).Days < 0)
            {
                Console.WriteLine("Cannot check out before check in");
                CheckInGuest(guestList, roomList);
            }
                
            else
            {
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
        }

        
    }

    catch (FormatException)
    {
        Console.WriteLine("Invalid date");
        CheckInGuest(guestList, roomList);
    }

}

void CheckOutGuest(List<Guest> guestList, List<Room> roomList)
{
    ShowGuestDetails(guestList);
    Guest CheckGuest = SearchGuest(guestList);
    if (CheckGuest.iSCheckedin == true)
    {
        try
        {
            
            double charge = CheckGuest.hotelStay.CalculateTotal(CheckGuest);
            Console.WriteLine("Total charge is: $" + charge);
            Console.WriteLine(CheckGuest.ToString());
            if (CheckGuest.membership.status == "Gold" || CheckGuest.membership.status == "Silver")
            {
                Console.WriteLine("You have {0} points redeemable. 1point = $1", CheckGuest.membership.points);
                Console.WriteLine("Do you wish to redeem your points to offset your total bill? [Y/N]");
                string Response = Console.ReadLine();
                if (Response.ToUpper() == "Y")
                {
                    Console.Write("Enter no. of points to redeem: ");
                    string redeem = Console.ReadLine();
                    try
                    {
                        int TryRedeem = Convert.ToInt32(redeem);
                        if (CheckGuest.membership.RedeemPoints(TryRedeem) == true)
                        {
                            Console.WriteLine("Press any key to make payment");
                            string UserKey = Console.ReadLine();
                            double NewPoint = CheckGuest.membership.EarnPoints(CheckGuest);
                            charge = charge - CheckGuest.membership.points;
                            Console.WriteLine("You have used {0} points to offset ${1} from your total bill. Total bill: ${2}", CheckGuest.membership.points, CheckGuest.membership.points, charge);
                            Console.WriteLine("You have earned {0} points", NewPoint);
                            CheckGuest.membership.points = Convert.ToInt32(NewPoint);
                            CheckGuest.iSCheckedin = false;
                            if (NewPoint >= 100 && CheckGuest.membership.status == "Silver")
                            {
                                Console.WriteLine("You have been promoted to the gold membership!!");
                                CheckGuest.membership.status = "Gold";
                                Console.WriteLine("\n");
                                Console.WriteLine("#################################");
                                Console.WriteLine("\n");
                                Console.WriteLine("Guest successfully checked out!");
                                Console.WriteLine("\n");
                                Console.WriteLine("#################################");
                                Console.WriteLine("\n\n");
                                ShowGuestDetails(guestList);
                            }

                            else
                            {
                                Console.WriteLine("\n");
                                Console.WriteLine("#################################");
                                Console.WriteLine("\n");
                                Console.WriteLine("Guest successfully checked out!");
                                Console.WriteLine("\n");
                                Console.WriteLine("#################################");
                                Console.WriteLine("\n\n");
                                ShowGuestDetails(guestList);
                            }
                        }

                        else
                        {
                            Console.WriteLine("You cannot redeem more points than you own");
                            CheckOutGuest(guestList, roomList);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Invalid input");
                        CheckOutGuest(guestList, roomList);
                    }
                    
                   /*Console.WriteLine("Press any key to make payment");
                    string UserKey = Console.ReadLine();
                    double NewPoint = CheckGuest.membership.EarnPoints(CheckGuest);
                    charge = charge - CheckGuest.membership.points;
                    Console.WriteLine("You have used {0} points to offset ${1} from your total bill. Total bill: ${2}", CheckGuest.membership.points, CheckGuest.membership.points, charge);
                    Console.WriteLine("You have earned {0} points", NewPoint);
                    CheckGuest.membership.points = Convert.ToInt32(NewPoint);
                    CheckGuest.iSCheckedin = false;*/
                   

                }

                else if (Response.ToUpper() == "N")
                {
                    double NewPoint = CheckGuest.membership.EarnPoints(CheckGuest);
                    Console.WriteLine("Total bill: ${0}", charge);
                    Console.WriteLine("Press any key to make payment");
                    string UserKey = Console.ReadLine();
                    Console.WriteLine("You have earned {0} points", NewPoint);
                    CheckGuest.membership.points = CheckGuest.membership.points + Convert.ToInt32(NewPoint);
                    CheckGuest.iSCheckedin = false;
                    if (NewPoint >= 100 && CheckGuest.membership.status == "Silver")
                    {
                        Console.WriteLine("You have been promoted to the gold membership!!");
                        CheckGuest.membership.status = "Gold";
                        Console.WriteLine("\n");
                        Console.WriteLine("#################################");
                        Console.WriteLine("\n");
                        Console.WriteLine("Guest successfully checked out!");
                        Console.WriteLine("\n");
                        Console.WriteLine("#################################");
                        Console.WriteLine("\n\n");
                        ShowGuestDetails(guestList);
                    }

                    else
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine("#################################");
                        Console.WriteLine("\n");
                        Console.WriteLine("Guest successfully checked out!");
                        Console.WriteLine("\n");
                        Console.WriteLine("#################################");
                        Console.WriteLine("\n\n");
                        ShowGuestDetails(guestList);
                    }
                }

                else
                {
                    Console.WriteLine("Invalid input");
                    CheckOutGuest(guestList, roomList);
                }
            }
            else
            {
                Console.WriteLine("You are unable to redeem points to offset your total bill until you have a higher membership status");
                double NewPoint = charge / 10;
                Console.WriteLine("Total bill: ${0}", charge);
                Console.WriteLine("Press any key to make payment");
                string UserKey = Console.ReadLine();
                Console.WriteLine("You have earned {0} points", NewPoint);
                CheckGuest.membership.points = CheckGuest.membership.points + Convert.ToInt32(NewPoint);

                if (NewPoint >= 100 && NewPoint < 200)
                {
                    Console.WriteLine("You have been promoted to the Silver membership!!");
                    CheckGuest.membership.status = "Silver";
                    Console.WriteLine("\n");
                    Console.WriteLine("#################################");
                    Console.WriteLine("\n");
                    Console.WriteLine("Guest successfully checked out!");
                    Console.WriteLine("\n");
                    Console.WriteLine("#################################");
                    Console.WriteLine("\n\n");
                    ShowGuestDetails(guestList);
                }

                else if (NewPoint >= 200)
                {
                    Console.WriteLine("You have been promoted to the gold membership!!");
                    CheckGuest.membership.status = "Gold";
                    Console.WriteLine("\n");
                    Console.WriteLine("#################################");
                    Console.WriteLine("\n");
                    Console.WriteLine("Guest successfully checked out!");
                    Console.WriteLine("\n");
                    Console.WriteLine("#################################");
                    Console.WriteLine("\n\n");
                    ShowGuestDetails(guestList);
                }

                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("#################################");
                    Console.WriteLine("\n");
                    Console.WriteLine("Guest successfully checked out!");
                    Console.WriteLine("\n");
                    Console.WriteLine("#################################");
                    Console.WriteLine("\n\n");
                    ShowGuestDetails(guestList);
                }
            }


           /* Console.WriteLine("\n");
            Console.WriteLine("#################################");
            Console.WriteLine("\n");
            Console.WriteLine("Guest successfully checked out!");
            Console.WriteLine("\n");
            Console.WriteLine("#################################");
            Console.WriteLine("\n\n");
            ShowGuestDetails(guestList);*/
        }

        catch (Exception)
        {
            Console.WriteLine("Invalid Input");
            CheckOutGuest(guestList, roomList);
        }
    }

    else
    {
        Console.WriteLine("Guest is not checked in.");
        CheckOutGuest(guestList, roomList);
    }
    
}
Guest SearchGuest(List<Guest> guestList)
{
    try
    {
        Console.WriteLine("Enter passport number: ");
        string PassNum = Console.ReadLine();
        ValidatePassport(PassNum);
        for (int i = 0; i < guestList.Count; i++)
        {
           
            if (guestList[i].passportNum == PassNum)
            {
                return guestList[i];
            }

            else
            {
                continue;
            }
        }
        Console.WriteLine("Passport number cannot be found");
        return SearchGuest(guestList);
        
    }
    catch (ArgumentNullException)
    {
        Console.WriteLine("Cannot enter empty input");
        return SearchGuest(guestList);
        
    }
    catch(ArgumentOutOfRangeException)
    {
        Console.WriteLine("Passport number length is too long");
        return SearchGuest(guestList);
        
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Input invalid characters");
        return SearchGuest(guestList);
        
    }
}

void ExtendStay(List<Guest> guestlist)
{
    ShowGuestDetails(guestlist);
    Guest guest = SearchGuest(guestlist);
    if (guest.iSCheckedin == false)
    {
        Console.WriteLine("Guest not checked in");
    }
    else
    {
        Console.Write("Enter number of days to extend: ");
        int Extend = Convert.ToInt32(Console.ReadLine());
        guest.hotelStay.checkoutDate = guest.hotelStay.checkoutDate.AddDays(Extend);
        Console.WriteLine("\n");
        Console.WriteLine("#################################");
        Console.WriteLine("\n");
        Console.WriteLine("Stay successfully extended");
        Console.WriteLine("\n");
        Console.WriteLine("#################################");
        Console.WriteLine("\n\n");
        return;
    }
}

void CalculateMonthlyCharges(List<Guest> guestList, IDictionary<string, double> monthlyCharges, string year)
{
    monthlyCharges.Keys.ToList().ForEach(k => monthlyCharges[k] = 0.00);
    for (int i = 0; i < guestList.Count; i++)
    {
        for (int x = 0; x < guestList[i].hotelStay.roomlist.Count; x++)
        {
            if (guestList[i].hotelStay.roomlist[x] is StandardRoom && guestList[i].hotelStay.checkoutDate.ToString("yyyy") == year)
            {
                Console.WriteLine("Standard");
                StandardRoom standard = (StandardRoom)guestList[i].hotelStay.roomlist[x];
                foreach(var item in monthlyCharges)
                {
                    if (guestList[i].hotelStay.checkoutDate.ToString("MMM") == item.Key)
                    {
                        Console.WriteLine(standard.CalculateCharges(guestList[i]));
                        monthlyCharges[item.Key] += standard.CalculateCharges(guestList[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
                
            }
            else if (guestList[i].hotelStay.roomlist[x] is DeluxeRoom && guestList[i].hotelStay.checkoutDate.ToString("yyyy") == year)
            {
                Console.WriteLine("Deluxe");
                DeluxeRoom deluxe = (DeluxeRoom)guestList[i].hotelStay.roomlist[x];
                foreach (var item in monthlyCharges)
                {
                    if (guestList[i].hotelStay.checkoutDate.ToString("MMM") == item.Key)
                    {
                        Console.WriteLine(deluxe.CalculateCharges(guestList[i]));
                        monthlyCharges[item.Key] += deluxe.CalculateCharges(guestList[i]);  
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                continue;
            }
        }
    }
}

void DisplayMonthlyCharges(IDictionary<string, double> monthlyCharges)
{
    try
    {
        Console.Write("Enter the year: ");
        string year = Console.ReadLine();
        ValidateYear(year);
        CalculateMonthlyCharges(guestList, monthlyCharges, year);
        foreach (var item in monthlyCharges)
        {
            Console.WriteLine(item.Key + ": " + item.Value);
        }
    }
    catch (ArgumentNullException Ne)
    {
        Console.WriteLine("A year is required for its monthly to be displayed");
        DisplayMonthlyCharges(monthlyCharges);
    }
    catch (ArgumentException Ae)
    {
        Console.WriteLine("Invalid year, please try again.");
    }
}


void Main()
{
    InitGuestData(guestList);
    InitRoomData(roomList);
    InitStayData(guestList, roomList);
    while (true)
    {
        Console.WriteLine("[1] List all guests \n[2] List all rooms \n[3] Register guest \n[4] CheckIn guest \n[5] List stay details \n[6] Extend Stay \n[7] Check Out Guest \n[8] Display Monthly Charges \n[0] Exit Program");
        string option = Console.ReadLine();

        try
        {
        switch (option)
        {
            case "1":  //Done By Nathan Farrel Lukito
                ShowGuestDetails(guestList);
                break;

            case "2":  //Done By Ervin Wong Yong Qi
                ShowRoomDetails(roomList);
                break;

            case "3": //Done By Nathan Farrel Lukito
                try
                {
                    RegisterGuest(guestList);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Cannot enter empty input");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Passport number too long");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input invalid characters");
                }
                break;

            case "4": //Done By Ervin Wong Yong Qi
                CheckInGuest(guestList, roomList);
                break;

            case "5": //Done By Nathan Farrel Lukito
                try
                {
                    ShowStayDetails(guestList);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Cannot enter empty input");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input invalid characters");
                }

                break;

            case "6":   //Done By Ervin Wong Yong Qi
                try
                {
                    ExtendStay(guestList);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Cannot enter empty input");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input invalid characters");
                }

                break;

            case "7": //Done By Ervin Wong Yong Qi
                try
                {
                    CheckOutGuest(guestList, roomList);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Cannot enter empty input");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input invalid characters");
                }

                break;

            case "8":
                DisplayMonthlyCharges(monthlyCharges); //Done By Nathan Farrel Lukito
                break;

            case "0":
                Console.WriteLine("Exiting Program... ...");
                return;
            default:
                throw new ArgumentException();
                break;
        }

    }
    catch(ArgumentException)
    {
       Console.WriteLine("Invalid Option, try again");
    }

    }   
}

Main();

