#import <AppKit/AppKit.h>
#import <Foundation/Foundation.h>

extern "C" {
 
    void OSXUniNClipboardSetPng(const char bytes[], const unsigned long length)
    {
        NSLog(@"Byte length: %lu\n", length);
        NSData *byteContent = [NSData dataWithBytes:bytes length:length];
        NSPasteboard *pasteboard = [NSPasteboard generalPasteboard];
        [pasteboard clearContents];
        [pasteboard declareTypes:@[NSPasteboardTypePNG] owner:nil];
        [pasteboard setData:byteContent forType:NSPasteboardTypePNG];
    }
}
