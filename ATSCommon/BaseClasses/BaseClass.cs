using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ATSCommon.BaseClasses
{
    [DataContract]
    public abstract class BaseClass
    {
        protected bool _IsDirty = false;
        protected bool _Initialized = true;
        protected ValidationResults validationResults;
        protected int customerId;


        /// <summary>
        /// Gets or sets a value indicating whether this instance is dirty [Modified and Unsaved].
        /// </summary>
        /// <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        [DataMember]
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                _IsDirty = value;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        {
            _Initialized = false;
        }

        public void EndInit()
        {
            _Initialized = true;
        }

        #endregion

        #region Helpers
        protected void FirePropertyChanged(string propName)
        {
            if (_Initialized)
            {
                _IsDirty = true;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }
        }

        protected virtual bool CheckPropertyChanged<T>(T member, T value) where T : class
        {
            if (member == null && value == null)
            {
                return false;
            }
            if (member != null)
            {
                return !member.Equals(value);
            }
            else
            {
                return !value.Equals(member);
            }
        }
        #endregion

        #region Validation

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        [DataMember]
        public bool IsValid
        {
            get { return Validate().IsValid; }
        }



        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>return list of validation results</returns>
   
        public ValidationResults Validate()
        {
            validationResults = ValidationFactory.CreateValidator(this.GetType())
                  .Validate(this);
            return validationResults;
        }

        /// <summary>
        /// Gets the validation error message from validation results.
        /// </summary>
        /// <value>The validation error message.</value>
        [XmlIgnore]
        [DataMember]
        public string ValidationErrorMessage
        {
            get
            {
                if (validationResults == null)
                {
                    Validate();
                }
                StringBuilder stringBuilder = new StringBuilder("Validation Results: \n");
                foreach (ValidationResult validationResult in validationResults)
                {
                    stringBuilder.Append(validationResult.Key + " : " + validationResult.Message + "\n");
                }
                return stringBuilder.ToString();
            }
        }
        #endregion

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
