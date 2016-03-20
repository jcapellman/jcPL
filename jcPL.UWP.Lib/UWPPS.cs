using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Storage;

using jcPL.PCL;

namespace jcPL.UWP.Lib {
    public class UWPPS : BasePS {
        private readonly bool _useRoaming;

        public UWPPS(bool useRoaming = true) {
            _useRoaming = useRoaming;
        }

        private StorageFolder SelectedStorageFolder => (_useRoaming ? ApplicationData.Current.RoamingFolder : ApplicationData.Current.LocalFolder);

        public override async Task<T> Get<T>(string fileName) {
            var filesinFolder = await SelectedStorageFolder.GetFilesAsync();

            var file = filesinFolder.FirstOrDefault(a => a.Name == fileName);

            if (file == null) {
                return default(T);
            }

            var buffer = await FileIO.ReadBufferAsync(file);
            
            return GetObjectFromBytes<T>(buffer.ToArray());
        }

        public override async Task<bool> Put<T>(string fileName, T fileData, bool replaceExisting = true) {
            var data = GetBytesFromT(fileData);

            using (var stream = await SelectedStorageFolder.OpenStreamForWriteAsync(fileName, (replaceExisting ? CreationCollisionOption.ReplaceExisting : CreationCollisionOption.OpenIfExists))) {
                stream.Write(data, 0, data.Length);
            }

            return true;
        }
    }
}