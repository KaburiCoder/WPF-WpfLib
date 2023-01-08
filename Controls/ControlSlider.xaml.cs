using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfLib.Controls
{
  /// <summary>
  /// ControlSlider.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class ControlSlider : UserControl, INotifyPropertyChanged
  {
    private ContentControl _backContent;
    private ContentControl _frontContent;

    private Storyboard _slideLeftToRight;
    private Storyboard _slideRightToLeft;
    private Storyboard _slideTopToBottom;
    private Storyboard _slideBottomToTop;

    private Duration? _duration;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void BeginSlideStoryboard(Storyboard storyboard)
    {
      Storyboard.SetTarget(storyboard.Children[0], _frontContent);
      Storyboard.SetTarget(storyboard.Children[1], _backContent);
      storyboard.Begin();
    }

    private void Slide_Complete(object sender, EventArgs e)
    {
      _backContent.Visibility = Visibility.Collapsed;
      _backContent = _frontContent;
    }

    public ControlSlider()
    {
      InitializeComponent();

      _slideLeftToRight = (Storyboard)Resources["SlideLeftToRight"];
      _slideRightToLeft = (Storyboard)Resources["SlideRightToLeft"];
      _slideTopToBottom = (Storyboard)Resources["SlideTopToBottom"];
      _slideBottomToTop = (Storyboard)Resources["SlideBottomToTop"];
      _backContent = content2;
      _frontContent = content1;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Duration? Duration
    {
      get
      {
        if (_duration == null)
        {
          _duration = new Duration(new TimeSpan(0, 0, 1));
        }
        return _duration;
      }
      set
      {
        if (_duration != value)
        {
          _duration = value;
          OnPropertyChanged();
        }
      }
    }

    public void SetAnimationSpeed(int milliseconds)
    {
      Duration = new Duration(new TimeSpan(0, 0, 0, 0, milliseconds));
    }

    public void InitContent(object newContent)
    {
      _frontContent.Content = null;
      _backContent.Content = newContent;
    }

    public void Slide(object newContent, SlideType slideType)
    {
      if (_backContent.Content == null)
      {
        InitContent(newContent);
        return;
      }

      _frontContent = _backContent == content2
        ? content1
        : content2;

      _frontContent.Visibility = Visibility.Visible;
      _frontContent.Content = newContent;

      switch (slideType)
      {
        case SlideType.LeftToRight:
          BeginSlideStoryboard(_slideLeftToRight);
          break;
        case SlideType.RightToLeft:
          BeginSlideStoryboard(_slideRightToLeft);
          break;
        case SlideType.TopToBottom:
          BeginSlideStoryboard(_slideTopToBottom);
          break;
        case SlideType.BottomToTop:
          BeginSlideStoryboard(_slideBottomToTop);
          break;
      }
    }
  }
}
