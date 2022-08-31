#import <AppKit/AppKit.h>
#import <Foundation/Foundation.h>

extern "C" {
 
    void OSXUniNClipboardSetPng(const char bytes[], const unsigned long length)
    {
        @try {
            NSData *byteContent = [NSData dataWithBytes:bytes length:length];
            NSPasteboard *pasteboard = [NSPasteboard generalPasteboard];
            [pasteboard clearContents];
            [pasteboard declareTypes:@[NSPasteboardTypePNG] owner:nil];
            [pasteboard setData:byteContent forType:NSPasteboardTypePNG];
        } @catch (NSException *exception) {
            NSLog(@"Could not save to clipboard: %@\nReason: %@", exception.name, exception.reason);
        }
    }
}
