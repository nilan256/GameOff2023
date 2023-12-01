using Doozy.Runtime.Bindy.Converters;

namespace Doozy.Runtime.Bindy
{

    public static partial class ConverterRegistry
    {

        static ConverterRegistry()
        {
            Converters.Add(new BoolToFloatConverter());
            Converters.Add(new BoolToIntConverter());
            Converters.Add(new BoolToStringConverter());
            Converters.Add(new ColorToStringConverter());
            Converters.Add(new DateTimeToStringConverter());
            Converters.Add(new EnumToStringConverter());
            Converters.Add(new FloatToBoolConverter());
            Converters.Add(new FloatToIntConverter());
            Converters.Add(new FloatToStringConverter());
            Converters.Add(new IntToBoolConverter());
            Converters.Add(new IntToFloatConverter());
            Converters.Add(new IntToShortConverter());
            Converters.Add(new IntToStringConverter());
            Converters.Add(new ShortToIntConverter());
            Converters.Add(new SpriteToTexture2DConverter());
            Converters.Add(new StringToBoolConverter());
            Converters.Add(new StringToColorConverter());
            Converters.Add(new StringToDateTimeConverter());
            Converters.Add(new StringToEnumConverter());
            Converters.Add(new StringToFloatConverter());
            Converters.Add(new StringToIntConverter());
            Converters.Add(new StringToVector2Converter());
            Converters.Add(new StringToVector3Converter());
            Converters.Add(new StringToVector4Converter());
            Converters.Add(new Texture2DtoSpriteConverter());
            Converters.Add(new Vector2ToStringConverter());
            Converters.Add(new Vector3ToStringConverter());
            Converters.Add(new Vector4ToStringConverter());
        }
        

    }

}