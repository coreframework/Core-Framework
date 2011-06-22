// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaptchaImage.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// <copyright file="CaptchaImage.cs" company="Managed Fusion, LLC">
//  Copyright (C) 2007-2008 Nicholas Berardi, Managed Fusion, LLC (nick@managedfusion.com)
// </copyright>
// <author>Nicholas Berardi</author>
// <author_email>nick@managedfusion.com</author_email>
// <company>Managed Fusion, LLC</company>
// <product>ASP.NET MVC CAPTCHA</product>
// <license>Microsoft Public License (Ms-PL)</license>
// <agreement>
// This software, as defined above in <product />, is copyrighted by the <author /> and the <company /> 
// and is licensed for use under <license />, all defined above.
// 
// This copyright notice may not be removed and if this <product /> or any parts of it are used any other
// packaged software, attribution needs to be given to the author, <author />.  This can be in the form of a textual
// message at program startup or in documentation (online or textual) provided with the packaged software.
// </agreement>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Web;

namespace Framework.MVC.Captcha
{
    /// <summary>
    /// Amount of random font warping to apply to rendered text.
    /// </summary>
    public enum FontWarpFactor
    {
        /// <summary>
        /// None font warp factor.
        /// </summary>
        None,

        /// <summary>
        /// Low font warp factor.
        /// </summary>
        Low,

        /// <summary>
        /// Medium font warp factor.
        /// </summary>
        Medium,

        /// <summary>
        /// High font warp factor.
        /// </summary>
        High,

        /// <summary>
        /// Extreme font warp factor.
        /// </summary>
        Extreme
    }

    /// <summary>
    /// Amount of background noise to add to rendered image.
    /// </summary>
    public enum BackgroundNoiseLevel
    {
        /// <summary>
        /// None background noise level.
        /// </summary>
        None,

        /// <summary>
        /// Low background noise level.
        /// </summary>
        Low,

        /// <summary>
        /// Medium background noise level.
        /// </summary>
        Medium,

        /// <summary>
        /// High background noise level.
        /// </summary>
        High,

        /// <summary>
        /// Extreme background noise level.
        /// </summary>
        Extreme
    }

    /// <summary>
    /// Amount of curved line noise to add to rendered image.
    /// </summary>
    public enum LineNoiseLevel
    {
        /// <summary>
        /// None line noise.
        /// </summary>
        None,

        /// <summary>
        /// Low line noise level.
        /// </summary>
        Low,

        /// <summary>
        /// Medium line noise level.
        /// </summary>
        Medium,

        /// <summary>
        /// High line noise level.
        /// </summary>
        High,

        /// <summary>
        ///  Extreme line noise level.
        /// </summary>
        Extreme
    }

    /// <summary>
    /// CAPTCHA Image.
    /// </summary>
    /// <seealso href="http://www.codinghorror.com">Original By Jeff Atwood</seealso>
    public class CaptchaImage
    {
        #region Fields

        /// <summary>
        /// Contains random font families.
        /// </summary>
        private static readonly string[] RandomFontFamily = { "arial", "arial black", "comic sans ms", "courier new", "estrangelo edessa", "franklin gothic medium", "georgia", "lucida console", "lucida sans unicode", "mangal", "microsoft sans serif", "palatino linotype", "sylfaen", "tahoma", "times new roman", "trebuchet ms", "verdana" };

        /// <summary>
        /// Contains random colors.
        /// </summary>
        private static readonly Color[] RandomColor = { Color.Red, Color.Green, Color.Blue, Color.Black, Color.Purple, Color.Orange };

        private readonly Random rand;
        private int height;
        private int width;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="CaptchaImage"/> class.
        /// </summary>
        static CaptchaImage()
        {
            FontWarp = FontWarpFactor.Medium;
            BackgroundNoise = BackgroundNoiseLevel.Low;
            LineNoise = LineNoiseLevel.Low;
            TextLength = 5;
            TextChars = "ACDEFGHJKLNPQRTUVXYZ2346789";
            CacheTimeOut = 180D;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaImage"/> class.
        /// </summary>
        public CaptchaImage()
        {
            rand = new Random();
            Width = 180;
            Height = 50;
            Text = GenerateRandomText();
            RenderedAt = DateTime.Now;
            UniqueId = Guid.NewGuid().ToString("N");
        }

        #endregion

        #region Static

        /// <summary>
        /// Gets or sets a string of available text characters for the generator to use.
        /// </summary>
        /// <value>The text chars.</value>
        public static string TextChars { get; set; }

        /// <summary>
        /// Gets or sets the length of the text.
        /// </summary>
        /// <value>The length of the text.</value>
        public static int TextLength { get; set; }

        /// <summary>
        /// Gets or sets amount of random warping to apply to the <see cref="CaptchaImage"/> instance.
        /// </summary>
        /// <value>The font warp.</value>
        public static FontWarpFactor FontWarp { get; set; }

        /// <summary>
        /// Gets or sets amount of background noise to apply to the <see cref="CaptchaImage"/> instance.
        /// </summary>
        /// <value>The background noise.</value>
        public static BackgroundNoiseLevel BackgroundNoise { get; set; }

        /// <summary>
        /// Gets or sets amount of line noise to apply to the <see cref="CaptchaImage"/> instance.
        /// </summary>
        /// <value>The line noise.</value>
        public static LineNoiseLevel LineNoise { get; set; }

        /// <summary>
        /// Gets or sets the cache time out.
        /// </summary>
        /// <value>The cache time out.</value>
        public static double CacheTimeOut { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a GUID that uniquely identifies this Captcha.
        /// </summary>
        /// <value>The unique id.</value>
        public string UniqueId { get; private set; }

        /// <summary>
        /// Gets the date and time this image was last rendered.
        /// </summary>
        /// <value>The rendered at.</value>
        public DateTime RenderedAt { get; private set; }

        /// <summary>
        /// Gets the randomly generated Captcha text.
        /// </summary>
        /// <value>The text of Captcha image.</value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets or sets the width of Captcha image to generate, in pixels.
        /// </summary>
        /// <value>The width of Captcha image.</value>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value <= 60)
                {
                    throw new ArgumentOutOfRangeException("width", value, "width must be greater than 60.");
                }
                    
                width = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of Captcha image to generate, in pixels.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value <= 30)
                {
                    throw new ArgumentOutOfRangeException("height", value, "height must be greater than 30.");
                }
                    
                height = value;
            }
        }

        #endregion

        /// <summary>
        /// Gets the cached captcha.
        /// </summary>
        /// <param name="guid">The GUID of the Captcha.</param>
        /// <returns>Cached captcha.</returns>
        public static CaptchaImage GetCachedCaptcha(string guid)
        {
            if (String.IsNullOrEmpty(guid))
            {
                return null;
            }

            return (CaptchaImage)HttpRuntime.Cache.Get(guid);
        }

        /// <summary>
        /// Forces a new Captcha image to be generated using current property value settings.
        /// </summary>
        /// <returns>Rendered captcha.</returns>
        public Bitmap RenderImage()
        {
            return GenerateImagePrivate();
        }

        /// <summary>
        /// Returns a GraphicsPath containing the specified string and font.
        /// </summary>
        /// <param name="s">The char value.</param>
        /// <param name="f">The font value.</param>
        /// <param name="r">The rectangle..</param>
        /// <returns>Text path.</returns>
        private static GraphicsPath TextPath(string s, Font f, Rectangle r)
        {
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            var gp = new GraphicsPath();
            gp.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, sf);
            return gp;
        }

        /// <summary>
        /// Returns a random font family from the font whitelist.
        /// </summary>
        /// <returns>Random font family.</returns>
        private string GetRandomFontFamily()
        {
            return RandomFontFamily[rand.Next(0, RandomFontFamily.Length)];
        }

        /// <summary>
        /// Generate random text for the CAPTCHA.
        /// </summary>
        /// <returns>Random text.</returns>
        private string GenerateRandomText()
        {
            var sb = new StringBuilder(TextLength);
            int maxLength = TextChars.Length;
            for (int n = 0; n <= TextLength - 1; n++)
            {
                sb.Append(TextChars.Substring(rand.Next(maxLength), 1));
            }
                
            return sb.ToString();
        }

        /// <summary>
        /// Returns a random point within the specified x and y ranges.
        /// </summary>
        /// <param name="xmin">The xmin value.</param>
        /// <param name="xmax">The xmax value .</param>
        /// <param name="ymin">The ymin value.</param>
        /// <param name="ymax">The ymax value.</param>
        /// <returns>Random point.</returns>
        private PointF RandomPoint(int xmin, int xmax, int ymin, int ymax)
        {
            return new PointF(rand.Next(xmin, xmax), rand.Next(ymin, ymax));
        }

        /// <summary>
        /// Randoms the color.
        /// </summary>
        /// <returns>Random color.</returns>
        private Color GetRandomColor()
        {
            return RandomColor[rand.Next(0, RandomColor.Length)];
        }

        /// <summary>
        /// Returns a random point within the specified rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns>A random point.</returns>
        private PointF RandomPoint(Rectangle rect)
        {
            return RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
        }

        /// <summary>
        /// Returns the CAPTCHA font in an appropriate size.
        /// </summary>
        /// <returns>The Captcha font.</returns>
        private Font GetFont()
        {
            float fsize;
            string fname = GetRandomFontFamily();

            switch (FontWarp)
            {
                case FontWarpFactor.None:
                    goto default;
                case FontWarpFactor.Low:
                    fsize = Convert.ToInt32(height * 0.8);
                    break;
                case FontWarpFactor.Medium:
                    fsize = Convert.ToInt32(height * 0.85);
                    break;
                case FontWarpFactor.High:
                    fsize = Convert.ToInt32(height * 0.9);
                    break;
                case FontWarpFactor.Extreme:
                    fsize = Convert.ToInt32(height * 0.95);
                    break;
                default:
                    fsize = Convert.ToInt32(height * 0.7);
                    break;
            }
            return new Font(fname, fsize, FontStyle.Bold);
        }

        /// <summary>
        /// Renders the CAPTCHA image.
        /// </summary>
        /// <returns>Bitmap image.</returns>
        private Bitmap GenerateImagePrivate()
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.Clear(Color.White);

                int charOffset = 0;
                double charWidth = width / TextLength;
                Rectangle rectChar;

                foreach (char c in Text)
                {
                    // establish font and draw area
                    using (Font fnt = GetFont())
                    {
                        using (Brush fontBrush = new SolidBrush(GetRandomColor()))
                        {
                            rectChar = new Rectangle(Convert.ToInt32(charOffset * charWidth), 0, Convert.ToInt32(charWidth), height);

                            // warp the character
                            GraphicsPath gp = TextPath(c.ToString(), fnt, rectChar);
                            WarpText(gp, rectChar);

                            // draw the character
                            gr.FillPath(fontBrush, gp);

                            charOffset += 1;
                        }
                    }
                }

                var rect = new Rectangle(new Point(0, 0), bmp.Size);
                AddNoise(gr, rect);
                AddLine(gr, rect);
            }

            return bmp;
        }

        /// <summary>
        /// Warp the provided text GraphicsPath by a variable amount.
        /// </summary>
        /// <param name="textPath">The text path.</param>
        /// <param name="rect">The rectangle.</param>
        private void WarpText(GraphicsPath textPath, Rectangle rect)
        {
            float warpDivisor;
            float rangeModifier;

            switch (FontWarp)
            {
                case FontWarpFactor.None:
                    goto default;
                case FontWarpFactor.Low:
                    warpDivisor = 6F;
                    rangeModifier = 1F;
                    break;
                case FontWarpFactor.Medium:
                    warpDivisor = 5F;
                    rangeModifier = 1.3F;
                    break;
                case FontWarpFactor.High:
                    warpDivisor = 4.5F;
                    rangeModifier = 1.4F;
                    break;
                case FontWarpFactor.Extreme:
                    warpDivisor = 4F;
                    rangeModifier = 1.5F;
                    break;
                default:
                    return;
            }

            RectangleF rectF;
            rectF = new RectangleF(Convert.ToSingle(rect.Left), 0, Convert.ToSingle(rect.Width), rect.Height);

            int hrange = Convert.ToInt32(rect.Height / warpDivisor);
            int wrange = Convert.ToInt32(rect.Width / warpDivisor);
            int left = rect.Left - Convert.ToInt32(wrange * rangeModifier);
            int top = rect.Top - Convert.ToInt32(hrange * rangeModifier);
            int width = rect.Left + rect.Width + Convert.ToInt32(wrange * rangeModifier);
            int height = rect.Top + rect.Height + Convert.ToInt32(hrange * rangeModifier);

            if (left < 0)
            {
                left = 0;
            }
            if (top < 0)
            {
                  top = 0;
            }
            if (width > Width)
            {
                 width = Width;
            }
            if (height > Height)
            {
                 height = Height;
            }
               
            PointF leftTop = RandomPoint(left, left + wrange, top, top + hrange);
            PointF rightTop = RandomPoint(width - wrange, width, top, top + hrange);
            PointF leftBottom = RandomPoint(left, left + wrange, height - hrange, height);
            PointF rightBottom = RandomPoint(width - wrange, width, height - hrange, height);

            var points = new[] { leftTop, rightTop, leftBottom, rightBottom };
            var m = new Matrix();
            m.Translate(0, 0);
            textPath.Warp(points, rectF, m, WarpMode.Perspective, 0);
        }

        /// <summary>
        /// Add a variable level of graphic noise to the image.
        /// </summary>
        /// <param name="g">The graphics.</param>
        /// <param name="rect">The rectangle.</param>
        private void AddNoise(Graphics g, Rectangle rect)
        {
            int density;
            int size;

            switch (BackgroundNoise)
            {
                case BackgroundNoiseLevel.None:
                    goto default;
                case BackgroundNoiseLevel.Low:
                    density = 30;
                    size = 40;
                    break;
                case BackgroundNoiseLevel.Medium:
                    density = 18;
                    size = 40;
                    break;
                case BackgroundNoiseLevel.High:
                    density = 16;
                    size = 39;
                    break;
                case BackgroundNoiseLevel.Extreme:
                    density = 12;
                    size = 38;
                    break;
                default:
                    return;
            }

            var br = new SolidBrush(GetRandomColor());
            int max = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / size);

            for (int i = 0; i <= Convert.ToInt32((rect.Width * rect.Height) / density); i++)
            {
                 g.FillEllipse(br, rand.Next(rect.Width), rand.Next(rect.Height), rand.Next(max), rand.Next(max));
            }
               
            br.Dispose();
        }

        /// <summary>
        /// Add variable level of curved lines to the image.
        /// </summary>
        /// <param name="g">The graphics.</param>
        /// <param name="rect">The rectangle.</param>
        private void AddLine(Graphics g, Rectangle rect)
        {
            int length;
            float width;
            int linecount;

            switch (LineNoise)
            {
                case LineNoiseLevel.None:
                    goto default;
                case LineNoiseLevel.Low:
                    length = 4;
                    width = Convert.ToSingle(height / 31.25);
                    linecount = 1;
                    break;
                case LineNoiseLevel.Medium:
                    length = 5;
                    width = Convert.ToSingle(height / 27.7777);
                    linecount = 1;
                    break;
                case LineNoiseLevel.High:
                    length = 3;
                    width = Convert.ToSingle(height / 25);
                    linecount = 2;
                    break;
                case LineNoiseLevel.Extreme:
                    length = 3;
                    width = Convert.ToSingle(height / 22.7272);
                    linecount = 3;
                    break;
                default:
                    return;
            }

            var pf = new PointF[length + 1];
            using (var p = new Pen(GetRandomColor(), width))
            {
                for (int l = 1; l <= linecount; l++)
                {
                    for (int i = 0; i <= length; i++)
                    {
                        pf[i] = RandomPoint(rect);
                    }
                        
                    g.DrawCurve(p, pf, 1.75F);
                }
            }
        }
    }
}