using InspectorModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InspectorTest
{
    public class TestModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [DirectoryInput("Root Path")]
        public static string RootPath { get; set; }

        public class TextObject : asd.TextObject2D, IListInput, INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            public string Name => Text.Replace("\r\n", " ");

            [TextAreaInput("Text")]
            public new string Text
            {
                get => base.Text;
                set
                {
                    base.Text = value;
                    OnPropertyChanged("Name");
                }
            }

            [VectorInput("Position")]
            public new asd.Vector2DF Position
            {
                get => base.Position;
                set => base.Position = value;
            }

            [TextInput("Angle")]
            public new float Angle
            {
                get => base.Angle;
                set => base.Angle = value;
            }

            [NumberInput("Order")]
            public new int DrawingPriority
            {
                get => base.DrawingPriority;
                set => base.DrawingPriority = value;
            }

            [BoolInput("Draw")]
            public new bool IsDrawn
            {
                get => base.IsDrawn;
                set => base.IsDrawn = value;
            }
        }

        [ListInput("TextObjects")]
        public ObservableCollection<TextObject> TextObjects { get; set; }

        [AddButtonMethodBinding("TextObjects")]
        public void AddText()
        {
            TextObject textObject = new TextObject();
            textObject.Font = asd.Engine.Graphics.CreateDynamicFont("", 50, new asd.Color(255, 255, 255), 0, new asd.Color());
            asd.Engine.AddObject2D(textObject);
            TextObjects.Add(textObject);
            OnPropertyChanged("TextCount");
        }

        [RemoveButtonMethodBinding("TextObjects")]
        public void RemoveText(TextObject textObject)
        {
            TextObjects.Remove(textObject);
            textObject.Dispose();
            OnPropertyChanged("TextCount");
        }

        public class TextureObject : asd.TextureObject2D, IListInput, INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            private string name;

            [RootPathBinding("root")]
            public string RootPath => TestModel.RootPath;

            [FileInput("Texture File", "PNG File|*.png", "root")]
            public string Name
            {
                get => name;
                set
                {
                    var texture = asd.Engine.Graphics.CreateTexture2D(value);
                    if (texture == null)
                        return;
                    name = value;
                    Texture = texture;
                    OnPropertyChanged();
                }
            }

            [VectorInput("Position")]
            public new asd.Vector2DF Position
            {
                get => base.Position;
                set => base.Position = value;
            }

            [VectorInput("Scale")]
            public new asd.Vector2DF Scale
            {
                get => base.Scale;
                set => base.Scale = value;
            }

            [TextInput("Angle")]
            public new float Angle
            {
                get => base.Angle;
                set => base.Angle = value;
            }

            [NumberInput("Order")]
            public new int DrawingPriority
            {
                get => base.DrawingPriority;
                set => base.DrawingPriority = value;
            }

            [BoolInput("Draw")]
            public new bool IsDrawn
            {
                get => base.IsDrawn;
                set => base.IsDrawn = value;
            }
        }

        [ListInput("TextureObjects")]
        public ObservableCollection<TextureObject> TextureObjects { get; set; }

        [AddButtonMethodBinding("TextureObjects")]
        public void AddTexture()
        {
            TextureObject textureObject = new TextureObject();
            asd.Engine.AddObject2D(textureObject);
            TextureObjects.Add(textureObject);
            OnPropertyChanged("TextureCount");
        }

        [RemoveButtonMethodBinding("TextureObjects")]
        public void RemoveTexture(TextureObject textureObject)
        {
            TextureObjects.Remove(textureObject);
            textureObject.Dispose();
            OnPropertyChanged("TextureCount");
        }

        [TextOutput("Text Cont")]
        public int TextCount => TextObjects.Count;

        [TextOutput("Texture Cont")]
        public int TextureCount => TextureObjects.Count;

        public TestModel()
        {
            TextObjects = new ObservableCollection<TextObject>();
            TextureObjects = new ObservableCollection<TextureObject>();
        }
    }
}
