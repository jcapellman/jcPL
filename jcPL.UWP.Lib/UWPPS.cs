using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Storage;

using jcPL.PCL;
using jcPL.PCL.Transports;

namespace jcPL.UWP.Lib {
    public class UWPPS : BasePS {
        private readonly bool _useRoaming;

        public UWPPS(bool useRoaming = true) {
            _useRoaming = useRoaming;
        }

        private StorageFolder SelectedStorageFolder => (_useRoaming ? ApplicationData.Current.RoamingFolder : ApplicationData.Current.LocalFolder);

        public override async Task<ReturnSet<T>> GetAsync<T>(string dataKey) {
            var filesinFolder = await SelectedStorageFolder.GetFilesAsync();

            var file = filesinFolder.FirstOrDefault(a => a.Name == dataKey);

            if (file == null) {
                return new ReturnSet<T>();
            }

            var buffer = await FileIO.ReadBufferAsync(file);

            return new ReturnSet<T>(GetObjectFromBytes<T>(buffer.ToArray()));
        }

        public override ReturnSet<T> Get<T>(string dataKey) {
            throw new NotImplementedException();
        }

        public override async Task<bool> PutAsync<T>(string dataKey, T fileData, bool replaceExisting = true) {
            var data = GetBytesFromT(fileData);

            using (var stream = await SelectedStorageFolder.OpenStreamForWriteAsync(dataKey, (replaceExisting ? CreationCollisionOption.ReplaceExisting : CreationCollisionOption.OpenIfExists))) {
                stream.Write(data, 0, data.Length);
            }

            return true;
        }

        public override bool Put<T>(string dataKey, T fileData, bool replaceExisting = true) {
            throw new NotImplementedException();
        }

        private async Task<Guid> generateUniqueGuid() {
            var filesinFolder = await SelectedStorageFolder.GetFilesAsync();

            var unique = false;
            var tmpGuid = Guid.NewGuid();

            do {
                var file = filesinFolder.FirstOrDefault(a => a.Name == tmpGuid.ToString());

                if (file == null) {
                    unique = true;
                } else {
                    tmpGuid = Guid.NewGuid();
                }
            } while (!unique);

            return tmpGuid;
        }

        public override Guid Put<T>(T fileData) { throw new NotImplementedException(); }

        public override async Task<Guid> PutAsync<T>(T fileData) {
            var returnGuid = await generateUniqueGuid();

            var result = await PutAsync(returnGuid.ToString(), fileData);

            return returnGuid;
        }
    }
}