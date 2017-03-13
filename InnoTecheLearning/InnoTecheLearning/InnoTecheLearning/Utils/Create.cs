﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InnoTecheLearning
{
    public static partial class Utils
    {
        /// <summary>
        /// A class that provides methods to help create the UI.
        /// </summary>
        public static class Create
        {
            public static Button ButtonU/*Uncoloured*/(Text Text, EventHandler OnClick, Color BackColor =
                default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button{Text = Text, TextColor = TextColor, BackgroundColor = BackColor};
                Button.Clicked += OnClick;
                return Button;
            }
            public static Button ButtonU/*Uncoloured*/(Text Text, EventHandler OnClick, Size Size,
                 Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button
                {
                    Text = Text,
                    TextColor = TextColor,
                    WidthRequest = Size.Width,
                    HeightRequest = Size.Height,
                    BackgroundColor = BackColor
                };
                Button.Clicked += OnClick;
                return Button;
            }
            public delegate void ButtonOnClick(ref Button sender, EventArgs e);
            public static Button Button(Text Text, ButtonOnClick OnClick, Color BackColor =
                default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button { Text = Text, TextColor = TextColor, BackgroundColor = BackColor };
                Button.Clicked += (sender, e) => { OnClick(ref Button, e); };
                return Button;
            }
            public static Button Button(Text Text, ButtonOnClick OnClick, Size Size,
                 Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button
                {
                    Text = Text,
                    TextColor = TextColor,
                    WidthRequest = Size.Width,
                    HeightRequest = Size.Height,
                    BackgroundColor = BackColor
                };
                Button.Clicked += (sender, e) => { OnClick(ref Button, e); };
                return Button;
            }
            public class ExpressionEventArgs : EventArgs
            {   public ExpressionEventArgs(Expressions Expression) : base() { this.Expression = Expression; }
                public Expressions Expression { get; } }
            public static Button Button(Expressions Expression, EventHandler<ExpressionEventArgs> OnClick, 
                Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button { Text = Expression.AsString(),
                    TextColor = TextColor, BackgroundColor = BackColor };
                Button.Clicked += (object sender, EventArgs e)=> { OnClick(sender, new ExpressionEventArgs(Expression)); };
                return Button;
            }
            public static Button Button(Expressions Expression, EventHandler<ExpressionEventArgs> OnClick, 
                Size Size, Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button
                {
                    Text = Expression.AsString(),
                    TextColor = TextColor,
                    WidthRequest = Size.Width,
                    HeightRequest = Size.Height,
                    BackgroundColor = BackColor
                };
                Button.Clicked += (object sender, EventArgs e) => { OnClick(sender, new ExpressionEventArgs(Expression)); };
                return Button;
            }
            public static Button Button(Expressions Expression, EventHandler<ExpressionEventArgs> OnClick,
                Text Text, Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button
                {
                    Text = Text,
                    TextColor = TextColor,
                    BackgroundColor = BackColor
                };
                Button.Clicked += (object sender, EventArgs e) => { OnClick(sender, new ExpressionEventArgs(Expression)); };
                return Button;
            }
            public static Button Button(Expressions Expression, EventHandler<ExpressionEventArgs> OnClick,
                Text Text, Size Size, Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Button = new Button
                {
                    Text = Text,
                    TextColor = TextColor,
                    WidthRequest = Size.Width,
                    HeightRequest = Size.Height,
                    BackgroundColor = BackColor
                };
                Button.Clicked += (object sender, EventArgs e) => { OnClick(sender, new ExpressionEventArgs(Expression)); };
                return Button;
            }
            [Obsolete("Use Create.Image(ImageSource Source, Action OnTap) instead.\nDeprecated in 0.10.0a46")]
            public static Button ButtonB(FileImageSource Image, EventHandler OnClick)
            {   return ButtonB(Image, OnClick, new Size(50, 50));}
            [Obsolete("Use Create.Image(ImageSource Source, Action OnTap, Size Size) instead.\nDeprecated in 0.10.0a46")]
            public static Button ButtonB(FileImageSource Image, EventHandler OnClick, Size Size)
            {
                Button Button = new Button
                {
                    Image = Image,
                    WidthRequest = Size.Width,
                    HeightRequest = Size.Height
                };
                Button.Clicked += OnClick;
                return Button;
            }

            [Obsolete("Use MainScreenItem(ImageSource Source, Action OnTap, Label Display) instead.\nDeprecated in 0.10.0a46")]
            public static StackLayout MainScreenItemB/*B = Button*/(FileImageSource Image, EventHandler OnClick, Text Display)
            {
                return new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    Children = { ButtonB(Image: Image, OnClick: OnClick), Display }
                };
            }

            public static StackLayout MainScreenItem(ImageSource Source, Action OnTap, Label Display)
            {
                return new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 70,
                    Children = { Image(Source: Source, OnTap: OnTap), Display }
                };
            }

            public static StackLayout MainScreenRow(params View[] MainScreenItems)
            {
                StackLayout MenuScreenRow = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Spacing = 50,
                    Children = { }
                };
                foreach (View MenuScreenItem in MainScreenItems)
                    MenuScreenRow.Children.Add(MenuScreenItem);
                return MenuScreenRow;
            }

            public static StackLayout MainScreenRow<T>(params T[] MainScreenItems) where T : View
            {
                StackLayout MenuScreenRow = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.StartAndExpand,
					Spacing = 50,
                    Children = { }
                };
                foreach (T MenuScreenItem in MainScreenItems)
                    MenuScreenRow.Children.Add(MenuScreenItem);
                return MenuScreenRow;
            }

            public static ImageSource Image(string FileName)
            {
                return ImageSource.FromResource(CurrentNamespace + ".Images." + FileName, typeof(Utils));
            }

            public enum ImageFile : int
            {
                Forum,
                Translate,
                VocabBook,
                Calculator,
                Calculator_Free,
                Factorizer,
                Sports,
                MusicTuner,
                MathSolver,
                Cello,
                Violin,
                Heart,
                Dragon,
                Dragon_Dead
            }


            public static ImageSource Image(ImageFile File)
            {
                string ActualFile = "";
                switch (File)
                {
                    case ImageFile.Forum:
                        ActualFile = "forum-message-3.png";
                        break;
                    case ImageFile.Translate:
                        ActualFile = "translator-tool-3.png";
                        break;
                    case ImageFile.VocabBook:
                        ActualFile = "book-2.png";
                        break;
                    case ImageFile.Calculator:
                        ActualFile = "square-root-of-x-mathematical-signs.png";
                        break;
                    case ImageFile.Calculator_Free:
                        ActualFile = "square-root-of-x-mathematical-signs.png";
                        break;
                    case ImageFile.Factorizer:
                        ActualFile = "mathematical-operation.png";
                        break;
                    case ImageFile.Sports:
                        ActualFile = "man-sprinting.png";
                        break;
                    case ImageFile.MusicTuner:
                        ActualFile = "treble-clef-2.png";
                        break;
                    case ImageFile.MathSolver:
                        ActualFile = "japanese-dragon.png";
                        break;
                    case ImageFile.Cello:
                        ActualFile = "cello-icon.png";
                        break;
                    case ImageFile.Violin:
                        ActualFile = "violin-icon.png";
                        break;
                    case ImageFile.Heart:
                        ActualFile = "8_bit_heart_stock_by_xquatrox-d4r844m.png";
                        break;
                    case ImageFile.Dragon:
                        ActualFile = "dragon.jpg";
                        break;
                    case ImageFile.Dragon_Dead:
                        ActualFile = "dragon.fw.png";
                        break;
                    default:
                        ActualFile = "";
                        break;
                }
                return Image(ActualFile);
                ;
            }
            public static Image Image(ImageFile File, Action OnTap)
            { return Image(Image(File), OnTap); }
            public static Image Image(ImageFile File, Action OnTap, Size Size)
            { return Image(Image(File), OnTap, Size); }
            public static Image ImageD/*D = Default (size)*/(ImageFile File, Action OnTap)
            { return ImageD(Image(File), OnTap); }
            public static Image ImageD/*D = Default (size)*/(ImageSource Source, Action OnTap)
            {
                Image Image = new Image{Source = Source};
                var Tap = new TapGestureRecognizer{ Command = new Command(OnTap) };
                Image.GestureRecognizers.Add(Tap);
                return Image;
            }
            public static Image Image(ImageSource Source, Action OnTap)
            {
                return Image(Source, OnTap, new Size(50, 50));
            }
            public static Image Image(ImageSource Source, Action OnTap, Size Size)
            {
                Image Image = new Image
                {
                    Source = Source,
                    WidthRequest = Size.Width,
                    HeightRequest = Size.Height
                };
                var Tap = new TapGestureRecognizer{ Command = new Command(OnTap) };
                Image.GestureRecognizers.Add(Tap);
                return Image;
            }
            public static Label BoldLabel(Text Text, Color TextColor = default(Color), 
                Color BackColor = default(Color), NamedSize Size = NamedSize.Default)
            {
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                if (TextColor == default(Color))
                    TextColor = Color.Default;
                return new Label
                {
                    Text = Text,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = TextColor,
                    BackgroundColor = BackColor,
                    VerticalTextAlignment = TextAlignment.Start,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(Size, targetElementType: typeof(Label)),
                    HorizontalOptions = LayoutOptions.Fill
                };
            }
            public static Label BoldLabel2(Text Text, Color TextColor = default(Color), 
                Color BackColor = default(Color), NamedSize Size = NamedSize.Default)
            {
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                if (TextColor == default(Color))
                    TextColor = Color.Default;
                return new Label
                {
                    FormattedText = Format(Bold(Text)),
                    TextColor = TextColor,
                    BackgroundColor = BackColor,
                    VerticalTextAlignment = TextAlignment.Start,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(Size, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Fill
                };
            }
            public static ScrollView Changelog
            {
                get
                {
                    var Return = new ScrollView
                    {
                        Content = new Label
                        {
                            Text = Resources.GetString("Change.log"),
                            TextColor = Color.Black,
                            LineBreakMode = LineBreakMode.WordWrap,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    /*Return.SizeChanged += (object sender, EventArgs e) =>
                    {
                        var View = (View)sender;
                        if (View.Width <= 0 || View.Height <= 0) return;
                        Return.WidthRequest = View.Width; Return.HeightRequest = View.Height;
                    };*/
                    return Return;
                }
            }
            public static Label VersionDisplay
            {
                get
                {
                    return new Label
                    {
                        Text = "Version: " + VersionFull,
                        HorizontalTextAlignment = TextAlignment.End,
                        VerticalTextAlignment = TextAlignment.Start,
                        LineBreakMode = LineBreakMode.NoWrap,
                        TextColor = Color.Black
                    };
                }
            }
            public static Button Back(Page Page, Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Return = Button("Back", delegate { Page.SendBackButtonPressed(); }, Color.Silver);
                Return.HorizontalOptions = LayoutOptions.End;
                Return.VerticalOptions = LayoutOptions.Fill;
                return Return;
            }/*
            [Obsolete("Not needed. Deprecated in 0.10.0a173")]
            public static Button UpdateAlpha(Page Page, Color BackColor = default(Color), Color TextColor = default(Color))
            {
                if (BackColor == default(Color))
                    BackColor = Color.Silver;
                if (TextColor == default(Color))
                    TextColor = Color.Black;
                Button Return = Button("Check for Alpha", delegate
                {
#if __ANDROID__
                    Android.App.ProgressDialog progress = new Android.App.ProgressDialog(Forms.Context);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Horizontal);
                    progress.SetMessage("Please wait... Loading updater....");
                    progress.SetCancelable(true);
                    progress.Show();
                    progress.SetMessage(new Updater((Updater.UpdateProgress Progress) =>
                    {
                        progress.SetMessage(Progress.ToName());
                        progress.Progress = Progress.Percentage() * 100;
                    }).CheckUpdate().ToString());
                    Do(System.Threading.Tasks.Task.Delay(2000));
                    progress.Dismiss();
#elif WINDOWS_UWP
                    var w = new ProgressDialog(
                        new ProgressDialogConfig { Title = "Please wait... Loading updater...." });
                    w.Show();
                    w.Title = new Updater((Updater.UpdateProgress Progress) =>
                    {
                        w.Title = Progress.ToName();
                        w.PercentComplete = Progress.Percentage();
                    }).CheckUpdate().ToString();
                    Do(System.Threading.Tasks.Task.Delay(2000));
                    w.Hide();
#else
                    Alert(Page, "Only supported on Android and Windows 10. " +
                        "For other versions, please check the github repository manually.");
#endif
                }, Color.Silver);
                Return.HorizontalOptions = LayoutOptions.Start;
                Return.VerticalOptions = LayoutOptions.Fill;
                return Return;
            }*/
            public static StackLayout ChangelogView(Page Page, Color BackColor = default(Color))
            {
                ScrollView Changelog = Create.Changelog;
                if (BackColor == default(Color))
                    BackColor = Color.White;
                return new StackLayout
                {
                    Children = { Changelog, Row(false, /*UpdateAlpha(Page),*/ Back(Page)) },
                    BackgroundColor = BackColor,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill
                };
            }
            public static Label Title(Text Text)
            {
                return new Label
                {
                    FontSize = 25,
                    BackgroundColor = Color.FromUint(4285098345),
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.Start,
                    Text = Text
                };
            }
            public static Label Society
            {
                get
                {
                    return new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Black,
                        FormattedText = Format((Text)"Developed by the\n", Bold("Innovative Technology Society of CSWCSS"))
                    };
                }
            }

            public static StackLayout Row(bool VerticalExpand, params View[] Items)
            {
                StackLayout MenuScreenRow = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = VerticalExpand ? LayoutOptions.StartAndExpand : LayoutOptions.Center,
                    Children = { }
                };
                foreach (View MenuScreenItem in Items)
                    MenuScreenRow.Children.Add(MenuScreenItem);
                return MenuScreenRow;
            }
            public static StackLayout Column(params View[] Items)
            {
                StackLayout MenuScreenRow = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Children = { }
                };
                foreach (View MenuScreenItem in Items)
                    MenuScreenRow.Children.Add(MenuScreenItem);
                return MenuScreenRow;
            }

            public static StackLayout Row<T>(bool VerticalExpand, params T[] Items) where T : View
            {
                StackLayout MenuScreenRow = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = VerticalExpand ? LayoutOptions.StartAndExpand : LayoutOptions.Center,
                    Children = { }
                };
                foreach (T MenuScreenItem in Items)
                    MenuScreenRow.Children.Add(MenuScreenItem);
                return MenuScreenRow;
            }
            public static StackLayout Column<T>(params T[] Items) where T : View
            {
                StackLayout MenuScreenRow = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Children = { }
                };
                foreach (T MenuScreenItem in Items)
                    MenuScreenRow.Children.Add(MenuScreenItem);
                return MenuScreenRow;
            }
            public static ScrollView ButtonStack(params Button[] Buttons)
            {
                StackLayout Return = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                for (int i = 0; i < Buttons.Length - 1; i++)
                    Return.Children.Add(new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children = { Buttons[i], Buttons[++i] }
                    });
                return new ScrollView
                {
                    Orientation = ScrollOrientation.Vertical,
                    Content = Return,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
            }
            public static ColumnDefinitionCollection Columns(GridUnitType Unit, params double[] Widths)
            {
                ColumnDefinitionCollection Return = new ColumnDefinitionCollection();
                foreach (int Width in Widths)
                    Return.Add(new ColumnDefinition { Width = new GridLength(Width, Unit) });
                return Return;
            }
            public static RowDefinitionCollection Rows(GridUnitType Unit, params double[] Heights)
            {
                RowDefinitionCollection Return = new RowDefinitionCollection();
                foreach (int Height in Heights)
                    Return.Add(new RowDefinition { Height = new GridLength(Height, Unit) });
                return Return;
            }
            public static Entry Entry(Text Text, Text Placeholder, Func<string> ReadOnly = null, Keyboard Keyboard = null,
                Color TextColor = default(Color), Color PlaceholderColor = default(Color), Color BackColor = default(Color))
            {
                Entry Return = new Entry
                {
                    Text = Text,
                    Placeholder = Placeholder,
                    Keyboard = Keyboard ?? Keyboard.Default,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = TextColor == default(Color) ? Color.Black : TextColor,
                    PlaceholderColor = PlaceholderColor == default(Color) ? Color.Silver : PlaceholderColor,
                    BackgroundColor = BackColor == default(Color) ? Color.Default : BackColor
                };
                if(ReadOnly!= null) Return.TextChanged += TextChanged(ReadOnly);
                return Return;
            }
            public static Version Version(int Major, int Minor, int Build = 0, VersionStage Stage = 0, short Revision = 0)
            { return new Version(Major, Minor, Build, (int)Stage * (1 << 16) + Revision); }
            public static Slider Slider(EventHandler<ValueChangedEventArgs> ValueChanged, 
               int Minimum = 0, int Maximum = 100, int Position = 100, Color BackColor = default(Color))
            {
                var Return = new Slider { Minimum = Minimum, Maximum = Maximum, Value = Position,
                    BackgroundColor = BackColor, HorizontalOptions = LayoutOptions.FillAndExpand };
                Return.ValueChanged += ValueChanged;
                return Return;
            }
            public static ScrollView RadioButtons(Color Base, Color Selected,
                Func<int, ButtonOnClick> Init, int DefaultIndex = 0, params string[] Names)
            {
                var Modificators = new Button[Names.Length];
                for (int Index = 0; Index < Names.Length; Index++)
                {
                    Modificators[Index] = Button(Names[Index], Init(Index), Index == DefaultIndex ? Selected : Base);
                    Modificators[Index].Clicked += (sender, e) => {
                        var Sender = sender as Button;
                        if (Sender.BackgroundColor == Base)
                        {
                            foreach (var Modify in Modificators)
                                Modify.BackgroundColor = Base;
                            Sender.BackgroundColor = Selected;
                        }
                        else Sender.BackgroundColor = Base;
                    };
                }
                ScrollView Modificator = new ScrollView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = ScrollOrientation.Horizontal,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    }
                };
                AppendScrollStack(Modificator, Modificators);
                return Modificator;
            }
            public static void AppendScrollStack<T>(ScrollView Base, params T[] Items) where T : View =>
                (Base.Content as StackLayout).Children.AddRange(Items);
            public static void FillGrid(Grid Base, View Item) => 
                Base.Children.Add(Item, 0, Base.RowDefinitions.Count, 0, Base.ColumnDefinitions.Count);
        }
    }
}