#import <UIKit/UIKit.h>
#import <MobileCoreServices/MobileCoreServices.h>

#if UNITY_VERSION <= 434
#import "iPhone_View.h"

#endif

@interface NonRotatingUIImagePickerController : UIImagePickerController


@end

@implementation NonRotatingUIImagePickerController
- (NSUInteger)supportedInterfaceOrientations{
    return UIInterfaceOrientationMaskLandscape;
}
@end


//-----------------------------------------------------------------

@interface APLViewController : UIViewController <UINavigationControllerDelegate, UIImagePickerControllerDelegate>{

    UIImagePickerController *imagePickerController;
@public
    const char *callback_game_object_name ;
    const char *callback_function_name ;
}

@end


@implementation APLViewController


- (void)viewDidLoad
{
    [super viewDidLoad];

    [self showImagePickerForSourceType:UIImagePickerControllerSourceTypePhotoLibrary];

}


- (void)showImagePickerForSourceType:(UIImagePickerControllerSourceType)sourceType
{
    imagePickerController = [[UIImagePickerController alloc] init];
    imagePickerController.modalPresentationStyle = UIModalPresentationCurrentContext;
    imagePickerController.sourceType = sourceType;
    imagePickerController.mediaTypes = [[NSArray alloc] initWithObjects:(NSString *)kUTTypeImage,nil];
    
    imagePickerController.delegate = self;
    
    [self.view addSubview:imagePickerController.view];
}


#pragma mark - UIImagePickerControllerDelegate

// This method is called when an image has been chosen from the library or taken from the camera.
- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info
{
    
  UIImage *image = [info objectForKey:UIImagePickerControllerOriginalImage];
  //  NSString *type = [info objectForKey:UIImagePickerControllerMediaType];
//    NSURL *urlvideo = [info objectForKey:UIImagePickerControllerReferenceURL];
//    NSString *urlString = [urlvideo absoluteString];
    
    // Image data:
    NSData *imageData = UIImageJPEGRepresentation(image,0.6);
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);

NSString *path=[[paths objectAtIndex:0] stringByAppendingPathComponent:@"temp.jpg"];
   [imageData writeToFile:path atomically:YES];
    
//    NSString *fileUri=[NSString stringWithFormat:@"file:%@?ori=%ld&cnt=%d",path,(long)image.imageOrientation,1];
    NSString *fileUri=[NSString stringWithFormat:@"file:%@",path];

    
   const char* video_url_path = [fileUri cStringUsingEncoding:NSASCIIStringEncoding];

    [self dismissViewControllerAnimated:YES completion:NULL];
    
    // UnitySendMessage("GameObject", "VideoPicked", video_url_path);
    UnitySendMessage(callback_game_object_name, callback_function_name, video_url_path);
}


- (void)imagePickerControllerDidCancel:(UIImagePickerController *)picker
{
    [self dismissViewControllerAnimated:YES completion:NULL];
}


@end





extern "C" {

    void OpenVideoPicker(const char *game_object_name, const char *function_name) {
        
        
        if ([UIImagePickerController isSourceTypeAvailable:UIImagePickerControllerSourceTypePhotoLibrary]) {
            // APLViewController
            UIViewController* parent = UnityGetGLViewController();
            APLViewController *uvc = [[APLViewController alloc] init];
            uvc->callback_game_object_name = strdup(game_object_name) ;
            uvc->callback_function_name = strdup(function_name) ;
            [parent presentViewController:uvc animated:YES completion:nil];
        }
    }
}
