#import <AppKit/AppKit.h>
#import <Foundation/Foundation.h>

extern "C" {
    
    const char *OSXUniNClipboardGetText()
    {
        NSString *textContent = [[NSPasteboard generalPasteboard] stringForType:NSPasteboardTypeString];
        if (!textContent)
            return NULL;
        
        // Allocate string in here
        // It should be freed automatically on the IL2CPP generated wrapper method => il2cpp_codegen_marshal_free
        const char *utf8TextContentString = [textContent UTF8String];
        char *utf8TextContentStringCopy = (char*)malloc(strlen(utf8TextContentString) + 1);
        strcpy(utf8TextContentStringCopy, utf8TextContentString);
        return utf8TextContentStringCopy;
    }
    
    void OSXUniNClipboardSetText(const char * text)
    {
        NSString *textContent = text != NULL ? [NSString stringWithUTF8String:text] : nil;
        NSPasteboard *pasteboard = [NSPasteboard generalPasteboard];
        [pasteboard clearContents];
        [pasteboard declareTypes:@[NSPasteboardTypeString] owner:nil];
        [pasteboard setString:textContent forType:NSPasteboardTypeString];
    }
 
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
