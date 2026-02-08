# Bok Interface

External tool to aid with routing and the research of Boktai games.

Our goals are to provide a

- simple interface that shows useful information while playing
- modify stats, inventory, etc.
- simulate damage calculations, forging, etc
- unlock and trigger events & other locked content

## What games and emulators are compatible?

The interface can detect all known versions of all Boktai games.  
It is mainly made for the japanese releases since those are relevant for the speedrun but it should work on all releases.

Bok Interface is made for BizHawk 2.9 and above but with [some adjustments](https://github.com/xDrHellx/Bok-Interface/tree/GBAHawk) it can also be compiled for GBAHawk.

## Usage

Download the latest release from the Assets section [here](https://github.com/xDrHellx/Bok-Interface/releases/latest) and extract it into your BizHawk folder.  
You should now have an "ExternalTools" folder with a "BokInterface.dll" file inside it.

The Bok Interface should now be available in BizHawk under "Tools" > "External Tool" > "Bok Interface".

## Development

Clone this repo, copy the BizHawk folder into the root, install the .NET SDK and run `./build.bat`.  
That will compile the code, copy the result to BizHawk and launch BizHawk with the Interface already loaded.

- BizHawk: <https://tasvideos.org/Bizhawk>
- .NET SDK: Run `winget install Microsoft.DotNet.SDK.8` or the installer from [here](https://dotnet.microsoft.com/en-us/download).

### Regarding memory addresses
- For GBA games, most memory addresses are the same across all versions.
- For DS games however, memory addresses are different between each version (JP, US, EU).
