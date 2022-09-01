# UniNClipboard
UniNClipboard is a minimalistic plugin for Unity 3D that abstracts the implementation of a basic Clipboard/Pasteboard for each platform. It allows to check the contents of the Clipboard, write content into it and, in some platforms, observe changes on it.

| Platform     | Strings            | Observe changes     | in Foreground | in Background   |
|----------    |----------------    |-----------------    |-----------    |------------     |
| Android      | Read and write     | Simple event        | Yes           | Yes             |
| iOS          | Read and write     | Simple event        | Yes           | Yes             |
| OSX          | Read and write     | WIP                 |               |                 |
| Windows      | TODO               | TODO                |               |                 |
| Linux        | TODO               | TODO                |               |                 |

# Installation
(WIP) ~~Just import the provided .unitypackage file~~

# Plugin Details
### Android
On Android, to keep it simple, we use the **JNI** implementation provided by Unity. This means no `.aar` library or Android Studio project is provided or required.
### iOS
On iOS, a implementation file called `UniNClipboardHelper.mm` is provided containing all the required implementation.
### Mac OSX
For Mac OSX, a compiled `UniNClipboard.bundle` is provided. If you would like to change or expand the implementation, the Xcode project is located in `OSX/UniNClipboard.xcodeproj`.
##### Building a new bundle (Instructions for Xcode 13.4.1)
- Import the project into Xcode.
- Click on Product -> Archive.
- From the "Archives" window that pops up, select the archive that was generated. Click on "Distribute Content" on the right.
- When asked to select a method of distribution, select "Built Products" and click on "Next".
- Select a location to save the bundle and click on "Export".
- Once the export completes, navigate to the selected location and drill down into the innermost directory. The bundle file should be generated.

### Windows
TODO?

### Linux
TODO?


# TODO
TODO/Ideas:

- [x] iOS Basic implementation (Only strings)
- [x] Android Basic implementation (Only strings)
- [ ] UnityPackage generation
- [ ] Compatibility for older Unity Versions (no assemblies)
- [x] OSX Basic implementation (Only strings)
- [ ] Windows Basic implementation (Only strings)
- [ ] Linux Basic implementation (Only strings)
- [ ] Add other types for iOS (Data? URL? HTML?)...
- [ ] Add other types for Android (Data? URL? HTML?)...
- [ ] Add other types for OSX (Data? URL? HTML?)...
- [ ] Add other types for Windows (Data? URL? HTML?)...
- [ ] Add other types for Linux (Data? URL? HTML?)...
