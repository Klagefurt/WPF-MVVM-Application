using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Xaml;

namespace WpfApp.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            var value_target_service = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_service = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

            OnInitialized(
                value_target_service?.TargetObject,
                value_target_service?.TargetProperty,
                root_object_service?.RootObject
                );

            return this;
        }

        private WeakReference _TargetRef;
        private WeakReference _RootRef;

        public object TargetObject => _TargetRef.Target;
        public object RootObject => _RootRef.Target;

        protected virtual void OnInitialized(object Target, object Property, object Root)
        {
            _TargetRef = new WeakReference(Target);
            _RootRef = new WeakReference(Root);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
