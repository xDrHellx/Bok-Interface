# Bok-Interface
External tool for Boktai games.

This is a tool mainly meant for making testing & routing easier.  
*For example testing impact from stats points in Boktai 2 & 3 can take a long time, as having higher stats can save time in multiple situations, which may end up being worth doing when all the time saved is accumulated.*

-------------------------------------

## Which versions of the games is this compatible with ?
The interface can detect all versions for each games.
However it is currently aimed at the **japanese versions**.

## Which BizHawk version is this compatible with ?
This interface is meant for BizHawk 2.9 and above.

## How do i add it to BizHawk ?
Create a *ExternalTools* folder inside your *BizHawk* folder and put the content of the release ZIP inside of it.
The Bok Interface should now be available within BizHawk under *Tools > External Tool > BokInterface*.

-------------------------------------

## The objectives
- Having a simple interface that shows useful information while playing
- Being able to change values "on the fly" via the external tool
- Simulations (damage calculation, etc)
- Enabling events & other locked contents

-------------------------------------

## Development

### How can i test changes to the interface ?
Either regenerate a new dll file or use the following command lines in VS Code (in this case you must fill the BizHawk folder with your version).
```
// Inside the BokInterface folder :
dotnet build

// Then go inside your BizHawk folder and type :
.\EmuHawk.exe --open-ext-tool-dll=BokInterface
```

*BizHawk should now start and automatically ask you if it can enable the Bok Interface.*
