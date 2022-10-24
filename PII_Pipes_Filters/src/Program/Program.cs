using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ejercicio 1

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture("..\\program\\beer.jpg");
            IFilter BWFilter = new FilterGreyscale();
            IFilter negativeFilter =new FilterNegative();
            IPipe nullPipe = new PipeNull();
            IPipe pipeFilter2 = new PipeSerial(negativeFilter, nullPipe);
            IPipe pipeFilter1 = new PipeSerial(BWFilter, pipeFilter2);
            //IPipe fork = new PipeFork(pipeFilter1, pipeFilter2 );
            IPicture filteredImage= pipeFilter1.Send(picture);
            provider.SavePicture(filteredImage, "..\\Program\\filteredImages\\filtered1.jpg");
            
            
            //Ejercicio2
            PictureProvider provider2 = new PictureProvider();
            IPicture picture2 = provider.GetPicture("..\\Program\\luke.jpg");
            PipeNull nullPipe2 = new PipeNull();

            FilterSave saver1BW =new FilterSave();
            FilterSave saver1Negative =new FilterSave();

            PipeSerial pipeStep2 = new PipeSerial(saver1Negative, nullPipe2);
            PipeSerial pipeNegative = new PipeSerial(new FilterNegative(), pipeStep2);
            PipeSerial pipeStep1 = new PipeSerial(saver1BW, pipeNegative);
            PipeSerial pipeGreyscale = new PipeSerial(new FilterGreyscale(), pipeStep1);

            saver1BW.PathChanger("..\\Program\\filteredImages\\lukeBW.jpg");
            saver1Negative.PathChanger("..\\Program\\filteredImages\\lukeNegative.jpg");
            IPicture filteredImage2= pipeGreyscale.Send(picture2);
            PictureProvider lastProvider2 = new PictureProvider();
            lastProvider2.SavePicture(filteredImage2, "..\\Program\\filteredImages\\filteredLuke.jpg");

            //Ejercicio 3
            
            PictureProvider provider3 = new PictureProvider();
            IPicture picture3 = provider.GetPicture("..\\Program\\2en4dias.jpg");
            PipeNull nullPipe3 = new PipeNull();

            FilterSave saver2BW =new FilterSave();
            FilterSave saver2Negative =new FilterSave();
            FilterTwitter twitterBW = new FilterTwitter();
            FilterTwitter twitterNegative = new FilterTwitter();


            PipeSerial pipeTwitter2 = new PipeSerial(twitterNegative, nullPipe3);
            PipeSerial pipeStep5 = new PipeSerial(saver2Negative, pipeTwitter2);
            PipeSerial pipeStep4 = new PipeSerial(new FilterNegative(), pipeStep5);
            PipeSerial pipeTwitter = new PipeSerial(twitterBW , pipeStep4);
            PipeSerial pipeStep3 = new PipeSerial(saver2BW, pipeTwitter);
            PipeSerial pipeGreyscale1 = new PipeSerial(new FilterGreyscale(), pipeStep3);

            saver2BW.PathChanger("..\\Program\\filteredImages\\2en4diasBW.jpg");
            saver2Negative.PathChanger("..\\Program\\filteredImages\\2en4diasNegative.jpg");
            twitterBW.Post = "Esa mancha no se borra nunca más";
            twitterBW.PathChanger("..\\Program\\filteredImages\\2en4diasBW.jpg");
            twitterNegative.Post = "Esa mancha no se borra nunca más";
            twitterNegative.PathChanger("..\\Program\\filteredImages\\2en4diasNegative.jpg");
            
            
            IPicture filteredImage3= pipeGreyscale1.Send(picture3);
            PictureProvider lastProvider3 = new PictureProvider();
            lastProvider3.SavePicture(filteredImage3, "..\\Program\\filteredImages\\filtered2en4dias.jpg");

            //Ejercicio 4

            PictureProvider provider4 = new PictureProvider();
            IPicture picture4 = provider.GetPicture("..\\Program\\indio.jpg");
            FilterTwitter twitterFilter = new FilterTwitter();
            
            PipeNull nullPipe4= new PipeNull();
            FilterConditional conditional = new FilterConditional();
            PipeSerial twtPipe = new PipeSerial(twitterFilter, nullPipe4);
            PipeSerial negative = new PipeSerial(new FilterNegative(), nullPipe4);
            PipeConditionalFork conditionalPipe = new PipeConditionalFork(conditional, negative, twtPipe);
            PipeSerial pipeGreyScaleCond = new PipeSerial(new FilterGreyscale(), conditionalPipe);
            
            twitterFilter.PathChanger("..\\Program\\indio.jpg");
            conditional.PathChanger("..\\Program\\indio.jpg");
            
            IPicture filteredImage4 =pipeGreyScaleCond.Send(picture4);
            PictureProvider lastProvider4 = new PictureProvider();
            lastProvider4.SavePicture(filteredImage4, "..\\Program\\filteredImages\\filteredIndio.jpg");







        }
    }
}
