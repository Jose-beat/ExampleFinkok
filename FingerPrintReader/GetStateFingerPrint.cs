using DPFP;
using DPFP.Capture;

namespace FingerPrintReader
{
    public class GetStateFingerPrint : DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
        public string stateFingerPrint()
        {
            string response = "";
            try
            {
                Capturer = new DPFP.Capture.Capture();

                if (Capturer != null)
                {
                    Capturer.EventHandler = this;
                    response =  startOperation();
                }
                else
                {
                    response =  "Can't initiate capture operation!";
                }





            }
            catch (Exception ex)
            {
                response =  "Ocurrio el error"  + ex.Message;
            }


            return response;
        }

        protected string startOperation()
        {
            try
            {
                Capturer.StartCapture();
                return "Captura iniciada";
            }catch (Exception ex)
            {
                return "Fallo al iniciar la captura" + ex.Message;
            }
        }

        protected virtual void Process(DPFP.Sample sample)
        {

        }
        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            throw new NotImplementedException();
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            throw new NotImplementedException();
        }
    }
}