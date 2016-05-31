///INCLUDES///////////////
#include <SoftwareSerial.h>
#include <openGLCD.h>
#include <Wire.h>
#include <SPI.h>
#include <Adafruit_PN532.h>
#include <Keypad.h>
#include <Time.h>
////////////////////////////

//Bitmap Objects//////////////////////////////////
Image_t bootLogo, blueTooth, NFC, battery, netwrk;
//Menu
Image_t Register, ScanId, Attendance, Edit;
//////////////////////////////////////////////////
int menu = 1;
int selectMenu = 0;
int btDataCount = 0;
char btData[16];
char wrtBandData[16];
bool IsDataWritten = false;
bool IsTagRead = false;
//

char buf[10]; //time buffer
//

//KEYPAD////////////////////////////////////////////////////////
const byte ROWS = 4; //four rows
const byte COLS = 4; //four columns
//define the cymbols on the buttons of the keypads

char hexaKeys[ROWS][COLS] = {
  {'1','2','3','A'},
  {'7','8','9','C'},
  {'4','5','6','B'},
  {'*','0','#','D'}
};
 
byte colPins[COLS] = {14, 15, 16, 17}; //connect to the column pinouts of the keypad
byte rowPins[ROWS] = {18, 19, 20, 21}; //connect to the row pinouts of the keypad //connect to the column pinouts of the keypad
/////////////////////////////////////////////////////////////////////

#define SCREEN_BRTNESS (44)

//NFC_PN5XX_AND_BLUETOOTH////////////////////////////////////////////////////////////
// If using the breakout with SPI, define the pins for SPI communication.
#define PN532_SCK  (2)
#define PN532_MOSI (3)
#define PN532_SS   (4)
#define PN532_MISO (5)

// If using the breakout or shield with I2C, define just the pins connected
// to the IRQ and reset lines.  Use the values below (2, 3) for the shield!
#define PN532_IRQ   (2)
#define PN532_RESET (3)  // Not connected by default on the NFC Shield

// Uncomment just _one_ line below depending on how your breakout or shield
// is connected to the Arduino:

// Use this line for a breakout with a SPI connection:
Adafruit_PN532 nfc(PN532_SCK, PN532_MISO, PN532_MOSI, PN532_SS);
SoftwareSerial BTSerial(51, 50);
//////////////////////////////////////////////////////////////////////////////////

//initialize an instance of class NewKeypad
Keypad customKeypad = Keypad( makeKeymap(hexaKeys), rowPins, colPins, ROWS, COLS); 


// Use this line for a breakout with a hardware SPI connection.  Note that
// the PN532 SCK, MOSI, and MISO pins need to be connected to the Arduino's
// hardware SPI SCK, MOSI, and MISO pins.  On an Arduino Uno these are
// SCK = 13, MOSI = 11, MISO = 12.  The SS line can be any digital IO pin.
//Adafruit_PN532 nfc(PN532_SS);

// Or use this line for a breakout or shield with an I2C connection:
//Adafruit_PN532 nfc(PN532_IRQ, PN532_RESET);
void setup()
{
  // Initialize the GLCD 
  GLCD.Init();
  pinMode(SCREEN_BRTNESS, OUTPUT);
  //digitalWrite(SCREEN_BRTNESS, HIGH);
  analogWrite(SCREEN_BRTNESS, 255);
  setDateTime(); // set date/time
  
  //Bitmaps
  bootLogo = logo; //Boot Logo
  blueTooth = bt;
  NFC = nfcico;
  battery = bat3; // battery 
  netwrk = network;
  //Menu BMP
  Register = register_;
  ScanId = scanId;
  Attendance = attendance;
  Edit = edit;
  playTone();
 // Select the font for the default text area
  GLCD.SelectFont(System5x7);
  screenInit();
  
  //Start all serial communication
  Serial.begin(115200);
  BTSerial.begin(9600);
  Serial.println("Hello!");
  

  nfc.begin();

  uint32_t versiondata = nfc.getFirmwareVersion();
  if (! versiondata) {
    //Serial.print("Didn't find PN53x board");
    while (1); // halt
  }
  // Got ok data, print it out!
  //Serial.print("Found chip PN5"); Serial.println((versiondata>>24) & 0xFF, HEX); 
  //Serial.print("Firmware ver. "); Serial.print((versiondata>>16) & 0xFF, DEC); 
  //Serial.print('.'); Serial.println((versiondata>>8) & 0xFF, DEC);
  
  // configure board to read RFID tags
  nfc.SAMConfig();
  
  //Serial.println("Waiting for an ISO14443A Card ...");
  GLCD.ClearScreen();
  //GLCD.print("Press 'A' to Scan Tag");
 // GLCD.CursorTo(0, 1);
 // GLCD.print("Press 'B' for Record");
  //BTSerial.println("Wave a tag");
  
  paintHomeScreen();
}

void loop()
{
 // readBluetooth();
  char homeKey = customKeypad.getKey();
  timeUpdate();
  if(homeKey == '1')
  {
    menu = 1;
    Menu();
  }
  
}

void screenInit()
{
    GLCD.print("Initializing.");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing..");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing...");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing.");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing..");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing...");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing.");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing..");
    delay(330);
    GLCD.ClearScreen();
    GLCD.print("Initializing...");
    delay(330);
    GLCD.ClearScreen();
}

void playTone()
{
  GLCD.DrawBitmap(bootLogo, 0,0);
  tone(38, 2000, 1000);
  delay(1000);
  tone(38, 1456, 2000);
  delay(500);
  tone(38, 3000, 1500);
  delay(2000);
  tone(38, 330, 200);
  GLCD.ClearScreen();
}

void buzz()
{
  tone(38, 4500, 500);
  //delay(1000);
}

void paintHomeScreen()
{
  GLCD.ClearScreen();
  GLCD.DrawBitmap(blueTooth, 0,0);
  GLCD.DrawBitmap(NFC, 16,0);
  GLCD.DrawBitmap(battery, 119,1);
  GLCD.DrawBitmap(netwrk, 108,0);
  GLCD.SelectFont(System5x7);
  GLCD.DrawString("1.Menu", 0, 57);
  GLCD.DrawString("A.Settings", 68, 57);
}

void Menu()
{
  //Default menu///////////////////
  REGISTER();
  
  //////////////////////////////////
  while(menu > 0)
  {
    char menuKey = customKeypad.getKey();
    /////////////////////////////////#
    if(menuKey == '#')
    {
      if(menu == 1)
      {
        SCANID();
        menu = 2;
      }
      else if(menu == 2)
      {
        ATTENDANCE();       
        menu = 3;
      }
      else if(menu == 3)
      {
        EDIT();
        menu = 4;
      }
      
    }
    /////////////////////////////////*
    if(menuKey == '*')
    {
      if(menu == 4)
      {
        ATTENDANCE();
        menu = 3;
      }
      else if(menu == 3)
      {
        SCANID();
        menu = 2;
      }
      else if(menu == 2)
      {
        REGISTER();
        menu = 1;
      }
    }
    /////////////////////////////////////back
    if(menuKey == 'A')
    {
        menu = 0;
        paintHomeScreen();
        break;    
    }
    /////////////////////////////////Selcet Menu
    if(menuKey == '1')
    {
      selectedMenu(menu);       
    }  
  }
  //////////////End of Menu 
}

void selectedMenu(int menuNo)
{
  switch(menuNo)
  {
    case 1 :
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      registerSelected();     
    break;
    case 2 :
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      scanidSelected();
    break;
    case 3 :
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      attendSelected();
    break;
    case 4 :
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      editSelected();
    break;
    case 0 :
    //paintHomeScreen();
    break;
  }
}

void registerSelected()
{   
      while(1)
      {
        char Key = customKeypad.getKey();
        if(BTSerial.available())
        {
          readBluetooth();
          GLCD.ClearScreen();
          GLCD.DrawString("A.Back", 86, 57);
          GLCD.DrawString("Scan Wrist Band", gTextfmt_center, gTextfmt_center);          
          while(IsDataWritten == false)
          {
            //readBluetooth();
            char rKey = customKeypad.getKey();
            //writeTag(btData);
            if(rKey == 'A')
            {
              REGISTER();
              //IsDataWritten = true;
              //return;
              break;
            }
            else
            {
              writeTag(btData);
            }
          }
          GLCD.ClearScreen();
          GLCD.DrawString("A.Back", 86, 57);
          GLCD.DrawString(wrtBandData, gTextfmt_center, gTextfmt_center);
          delay(4000);
          GLCD.DrawString("Student Registered", gTextfmt_center, gTextfmt_center);
          delay(4000);
          REGISTER();
          //emtData(wrtBandData);
          //memset(&wrtBandData[0], '\0', sizeof(wrtBandData));
          IsDataWritten = false;
          break;
        }
        else
        {
          //GLCD.ClearScreen();
          GLCD.DrawString("A.Back", 86, 57);
          GLCD.DrawString("Waiting For Data...", gTextfmt_center, gTextfmt_center);
        }
        if(Key == 'A')
        {
          REGISTER();
          break;
        }
      } 
}

void scanidSelected()
{
  while(1)
  {
    char Key = customKeypad.getKey();
    
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      GLCD.DrawString("Scan Wrist Band", gTextfmt_center, gTextfmt_center);          
      while(IsTagRead == false)
      {
        char sKey = customKeypad.getKey();
        if(Key == 'A')
        {
          SCANID();
          break;
        } 
        else
        {
           readTag();
        }    
        
      }
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      GLCD.DrawString(wrtBandData, gTextfmt_center, gTextfmt_center);
      BTSerial.println(wrtBandData);
      delay(2000);
          //GLCD.DrawString("Student Registered", gTextfmt_center, gTextfmt_center);
          //delay(4000);
      SCANID();
      IsTagRead = false;
      //emtData(wrtBandData);
      break;
    if(Key == 'A')
    {
      SCANID();
      break;
    }
  } 
}

void attendSelected()
{
  while(1)
  {
    char Key = customKeypad.getKey();
    
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      GLCD.DrawString("Scan Wrist Band", gTextfmt_center, gTextfmt_center);          
      while(IsTagRead == false)
      {
        char aKey = customKeypad.getKey();
        readTag();
        if(aKey == 'A')
        {
          ATTENDANCE();
          break;
        }
      }
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      GLCD.DrawString(wrtBandData, gTextfmt_center, gTextfmt_center);
      BTSerial.println(wrtBandData);
      delay(2000);
          //GLCD.DrawString("Student Registered", gTextfmt_center, gTextfmt_center);
          //delay(4000);
      ATTENDANCE();
      IsTagRead = false;
      break;
    if(Key == 'A')
    {
      ATTENDANCE();
      break;
    }
  } 
}

void editSelected()
{
  while(1)
  {
    char Key = customKeypad.getKey();
    
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      GLCD.DrawString("Scan Wrist Band", gTextfmt_center, gTextfmt_center);          
      while(IsTagRead == false)
      {
        char eKey = customKeypad.getKey();
        readTag();
        if(eKey == 'A')
        {
          EDIT();
          break;
        }
      }
      GLCD.ClearScreen();
      GLCD.DrawString("A.Back", 86, 57);
      GLCD.DrawString(wrtBandData, gTextfmt_center, gTextfmt_center);
      BTSerial.println(wrtBandData);
      delay(2000);
          //GLCD.DrawString("Student Registered", gTextfmt_center, gTextfmt_center);
          //delay(4000);
      EDIT();
      IsTagRead = false;
      break;
    if(Key == 'A')
    {
      EDIT();
      break;
    }
  } 
}

void REGISTER()
{
  GLCD.ClearScreen();
  GLCD.DrawBitmap(battery, 119,1);
  GLCD.DrawBitmap(netwrk, 108,0);
  GLCD.DrawBitmap(Register, 0,8);
  GLCD.SelectFont(System5x7);
  GLCD.DrawString("1.Select", 0, 57);
  GLCD.DrawString("A.Back", 86, 57);
  menu = 1;

}
void SCANID()
{
  GLCD.ClearScreen();
  GLCD.DrawBitmap(battery, 119,1);
  GLCD.DrawBitmap(netwrk, 108,0);
  GLCD.DrawBitmap(ScanId, 0,8);
  GLCD.SelectFont(System5x7);
  GLCD.DrawString("1.Select", 0, 57);
  GLCD.DrawString("A.Back", 86, 57);
  menu = 2;
}

void ATTENDANCE()
{
  GLCD.ClearScreen();
  GLCD.DrawBitmap(battery, 119,1);
  GLCD.DrawBitmap(netwrk, 108,0);
  GLCD.DrawBitmap(Attendance, 0,8);
  GLCD.SelectFont(System5x7);
  GLCD.DrawString("1.Select", 0, 57);
  GLCD.DrawString("A.Back", 86, 57);
  menu = 3;
}

void EDIT()
{
  GLCD.ClearScreen();
  GLCD.DrawBitmap(battery, 119,1);
  GLCD.DrawBitmap(netwrk, 108,0);
  GLCD.DrawBitmap(Edit, 0,8);
  GLCD.SelectFont(System5x7);
  GLCD.DrawString("1.Select", 0, 57);
  GLCD.DrawString("A.Back", 86, 57);
  menu = 4;
}

/////File Transfer Bluetooth/////////////////////////////////
void readBluetooth()
{
  btDataCount = 0;
  //BTSerial.flush();
  //memset(&btData[0], '\0', sizeof(btData));
  while (BTSerial.available())
  {
    btData[btDataCount] = BTSerial.read(); 
    if(btDataCount == 14)
    {
      Serial.println(btData);
      btDataCount = 0;
      BTSerial.flush();
      break;
    }
    
    ++btDataCount;
  }
}

void emtData(char *delData)
{
  for(int i = 0; i < 16; i++)
  {
    delData[i] = '0';
  }
}

void writeTag(char *chunkData)
{
  char wKey = customKeypad.getKey();
  if(wKey == 'A')
  {
    
  }
  uint8_t success;
  uint8_t uid[] = { 0, 0, 0, 0, 0, 0, 0 };  // Buffer to store the returned UID
  uint8_t uidLength;                        // Length of the UID (4 or 7 bytes depending on ISO14443A card type)
  //memset(&wrtBandData[0], '\0', sizeof(wrtBandData));
  // Wait for an ISO14443A type cards (Mifare, etc.).  When one is found
  // 'uid' will be populated with the UID, and uidLength will indicate
  // if the uid is 4 bytes (Mifare Classic) or 7 bytes (Mifare Ultralight)
  success = nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength);
  
  if (success) {
    // Display some basic information about the card
    //Serial.println("Found an ISO14443A card");
    //Serial.print("  UID Length: ");Serial.print(uidLength, DEC);Serial.println(" bytes");
    //Serial.print("  UID Value: ");
    //nfc.PrintHex(uid, uidLength);
    //Serial.println("");
    
    if (uidLength == 4)
    {
      // We probably have a Mifare Classic card ... 
      //Serial.println("Seems to be a Mifare Classic card (4 byte UID)");
	  
      // Now we need to try to authenticate it for read/write access
      // Try with the factory default KeyA: 0xFF 0xFF 0xFF 0xFF 0xFF 0xFF
      //Serial.println("Trying to authenticate block 4 with default KEYA value");
      uint8_t keya[6] = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
	  
	  // Start with block 4 (the first block of sector 1) since sector 0
	  // contains the manufacturer data and it's probably better just
	  // to leave it alone unless you know what you're doing
      success = nfc.mifareclassic_AuthenticateBlock(uid, uidLength, 4, 0, keya);
	  
      if (success)
      {
        //Serial.println("Sector 1 (Blocks 4..7) has been authenticated");
        uint8_t data[16];
		
        // If you want to write something to block 4 to test with, uncomment
		// the following line and this text should be read back in a minute
        memcpy(data, chunkData, sizeof data);
         success = nfc.mifareclassic_WriteDataBlock (4, data);

        // Try to read the contents of block 4
        success = nfc.mifareclassic_ReadDataBlock(4, data);
		
        if (success)
        {
          // Data seems to have been read ... spit it out
          Serial.println("Reading Block 4:");
          //nfc.PrintHexChar(data, 16);
          for(uint16_t i=0; i<14; i++)
          {
            wrtBandData[i] = (char)data[i];
          }
         // Serial.println(wrtBandData);
          //Serial.println("");
          IsDataWritten = true;  
          // Wait a bit before reading the card again
          buzz();
          
          //emtData(btData);
          delay(1000);
        }
        else
        {
          IsDataWritten = false;
          //Serial.println("Ooops ... unable to read the requested block.  Try another key?");
        }
      }
      else
      {
        GLCD.ClearScreen();
        GLCD.SelectFont(System5x7);
        GLCD.DrawString("Authentication Failed!", 0, 57);
        REGISTER();
        return;
        //Serial.println("Ooops ... authentication failed: Try another key?");
      }
    }
  }
}

void readTag()
{
  uint8_t success;
  uint8_t uid[] = { 0, 0, 0, 0, 0, 0, 0 };  // Buffer to store the returned UID
  uint8_t uidLength;                        // Length of the UID (4 or 7 bytes depending on ISO14443A card type)
    
  // Wait for an ISO14443A type cards (Mifare, etc.).  When one is found
  // 'uid' will be populated with the UID, and uidLength will indicate
  // if the uid is 4 bytes (Mifare Classic) or 7 bytes (Mifare Ultralight)
  success = nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, uid, &uidLength);
  
  if (success) {
    
    if (uidLength == 4)
    {
      uint8_t keya[6] = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
	  
	  // Start with block 4 (the first block of sector 1) since sector 0
	  // contains the manufacturer data and it's probably better just
	  // to leave it alone unless you know what you're doing
      success = nfc.mifareclassic_AuthenticateBlock(uid, uidLength, 4, 0, keya);
	  
      if (success)
      {
        uint8_t data[16];

        // Try to read the contents of block 4
        success = nfc.mifareclassic_ReadDataBlock(4, data);
		
        if (success)
        {
          // Data seems to have been read ... spit it out
          //nfc.PrintHexChar(data, 16);
          for(uint16_t i=0; i<14; i++)
          {
            wrtBandData[i] = (char)data[i];
          }
          IsTagRead = true;  
          // Wait a bit before reading the card again
          buzz();
          delay(1000);
        }
        else
        {
          IsTagRead = false;
          //Serial.println("Ooops ... unable to read the requested block.  Try another key?");
        }
      }
      else
      {
        GLCD.ClearScreen();
        GLCD.SelectFont(System5x7);
        GLCD.DrawString("Authentication Failed!", 0, 57);
        SCANID();
        return;
        //Serial.println("Ooops ... authentication failed: Try another key?");
      }
    }
  }
}

//// Time Lib//////////////////////////////////////////////////////////////////////
tmElements_t tm;

/*
 * Set DateTime for Time library
 */
void setDateTime(void)
{
	getDate(__DATE__); // get compiled date
	getTime(__TIME__); // get compiled time

	// set Time library DateTime to extracted date & time
	setTime(tm.Hour,tm.Minute,tm.Second, tm.Day, tm.Month, tm.Year);
}

const char *monthName[12] = {
  "Jan", "Feb", "Mar", "Apr", "May", "Jun",
  "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
};

bool getTime(const char *str)
{
int Hour, Min, Sec;

  if (sscanf(str, "%d:%d:%d", &Hour, &Min, &Sec) != 3)
    return false;
  tm.Hour = Hour;
  tm.Minute = Min;
  tm.Second = Sec;
  return true;
}

bool getDate(const char *str)
{
char Month[12];
int Day, Year;
uint8_t monthIndex;

  if (sscanf(str, "%s %d %d", Month, &Day, &Year) != 3) return false;
  for (monthIndex = 0; monthIndex < 12; monthIndex++)
  {
    if (strcmp(Month, monthName[monthIndex]) == 0)
      break;
  }
  if (monthIndex >= 12)
    return false;
  tm.Day = Day;
  tm.Month = monthIndex + 1;
  tm.Year = CalendarYrToTm(Year);
  return true;
}

void timeUpdate()
{
  GLCD.SelectFont(lcdnums14x24);// LCD looking font
  static time_t  prevtime;
//char buf[10];

	if( prevtime != now() ) // if 1 second has gone by, update display
	{
		prevtime = now();   // save the last update time

		// format the time in a buffer
		snprintf(buf, sizeof(buf), "%02d:%02d:%02d", hour(), minute(), second());

		// draw the formatted string centered on the display
  		GLCD.DrawString(buf, gTextfmt_center, gTextfmt_center);
	}
}
/////End Time Lib//////////////////////////////////////////////////////////////////////
