using Xamarin.Forms;

namespace Pressed.Pressed.Extensions
{
    public static class FormsExtensions
    {
        // all of this because I'm lazy :/
        public static bool Is<T>(this Effect effect) => effect.GetType() == typeof(T);
        public static bool Is<T>(this Effect effect, out T outEffect) where T:class
        {
            outEffect = effect as T;
            return outEffect != null;
        }
        public static bool Is<T>(this IGestureRecognizer gestureRecognizer) => gestureRecognizer.GetType() == typeof(T);
        
        public static bool Is<T>(this Element element) => element.GetType() == typeof(T);
        public static bool Is<T>(this Element element, out T outElement) where T : class
        {
            outElement = element as T;
            return outElement != null;
        }
    }
}