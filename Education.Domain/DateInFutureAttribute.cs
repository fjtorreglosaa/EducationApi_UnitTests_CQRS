﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain
{
    public class DateInFutureAttribute : ValidationAttribute
    {
        private readonly Func<DateTime> _dateNowProvider;

        public DateInFutureAttribute(): this(() => DateTime.Now) { }

        public DateInFutureAttribute(Func<DateTime> dateNowProvider)
        {
            _dateNowProvider = dateNowProvider;
            ErrorMessage = "La fecha debe ser del futuro";
        }

        public override bool IsValid(object? value)
        {
            bool isValid = false;

            if (value is DateTime dateTime)
            {
                isValid = dateTime > _dateNowProvider();
            }

            return isValid;
        }
    }
}
