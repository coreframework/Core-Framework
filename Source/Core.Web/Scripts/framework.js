/**
  *  Extends javascript basic objects (Function, String, Boolean).
  *
  *  Copyright (c) 2008 Roman Zaharenkov
  *  Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php) 
  *  and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
  */
	
/**	
  * 	Replaces each format item in a specified String with the text equivalent of a corresponding object's value.
  * 	
  * 	Paramaters:
  *		  template - A composite format string.
  *	    args - An Object array containing zero or more objects to format. 
  *
  *   Examples:
  *		  String.format('Format {0} example with {1} arguments.', 'string', 2); => 'Format string example with 2 arguments.'
  */	
String.format = function(template, args) {

	result = arguments[0];
	
	for (i = 1; i < arguments.length; i++) {      
		result = result.replace(new RegExp('\\{' + (i - 1) + '\\}', 'gm'), arguments[i]);
	}

	return result;
};

/**	
  * 	Shring string if it's length > maxLength.
  * 	
  * 	Paramaters:
  *		  maxLength - maximum length of output string.
  *	    endWith - end of string (if it's length exceed maxLength). '...' - default.
  *
  *   Examples:
  *		  String.shrink('Lorem ipsum dolor sit amet, consectetuer volutpat.', 10); => 'Lorem ipsu...'
  *		  String.shrink('Lorem ipsum dolor sit amet, consectetuer volutpat.', 10, '...the end); => 'Lorem ipsu...the end'
  */	
String.shrink = function(inputString, maxLength, endWith) {

  if (!endWith) {
    endWith = '...';
  }

  if (inputString.length <= maxLength) {
    return inputString;
  }
  else {
    return inputString.substring(0, maxLength - 1) + endWith;
  }
};

/**	
  * 	Replaces each format item in a specified String with the text equivalent of a corresponding object's value.
  * 	
  * 	Paramaters:
  *		  template - A composite format string.
  *	    args - An Object array containing zero or more objects to format. 
  *
  *   Examples:
  *		  'Format {0} example with {1} arguments.'.format('string', 2); => 'Format string example with 2 arguments.'
  */	
String.prototype.format = function(args) {

	result = this;
	
	for (i = 0; i < arguments.length; i++) {      
		result = result.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
	}

	return result;
};

/**	
  * 	Shring string if it's length > maxLength.
  * 	
  * 	Paramaters:
  *		  maxLength - maximum length of output string.
  *	    endWith - end of string (if it's length exceed maxLength). '...' - default.
  *
  *   Examples:
  *		  'Lorem ipsum dolor sit amet, consectetuer volutpat.'.shrink(10); => 'Lorem ipsu...'
  *		  'Lorem ipsum dolor sit amet, consectetuer volutpat.'.shrink(10, '...the end); => 'Lorem ipsu...the end'
  */	
String.prototype.shrink = function(maxLength, endWith) {

  if (!endWith) {
    endWith = '...';
  }

  if (this.length <= maxLength) {
    return this;
  }
  else {
    return this.substring(0, maxLength - 1) + endWith;
  }
};

/**	
  *  	CSharp like string builder. This class represents a string-like object whose value is a mutable sequence of characters.
  *	  
  *	  Examples:
  *		  style = new StringBuilder();
  *		  style.appendFormat('width:{0}px;', 100);				
  *		  style.appendFormat('margin-left:-{0}px;', '20');
  *		  style.appendFormat('top:{0}px;', 10 / 2);
  *		  style.appendFormat('left:{0};', '50%');
  *		  return style.toString(); => 'width:100px;margin-left:20px;top:5px;left:50%;'
  */
StringBuilder = function(value) {
	this.strings = new Array();
	this.append(value);
};

StringBuilder.prototype = {
	
	/**
	*   Appends the string representation of a specified object to the end of this instance.
	* 	
	*   Paramaters:
	*		  value - The object to append. 
	*/	
	append : function (value) {
		if (value) { 
			this.strings.push(value); 
		}
	},	
	
	/**
	*	Appends a formatted string, which contains zero or more format specifications, to this instance. Each format specification is replaced by the string representation of a corresponding object argument.
	* 	
	* Paramaters:
	*		template - A composite format string.
	*	  args - An Object array containing zero or more objects to format. 
	*/
	appendFormat : function (template, args) {
		this.append(String.format(arguments));
	},

	/**
	*	Clears the instance.
	*/
	clear : function () {
		this.strings.length = 0;
	},
	
	/**
	*	 Converts the value of a StringBuilder to a String.
	*/
	toString : function () {
		return this.strings.join('');
	}
};

/**
  *  Creates function delegate.
  *   
	* Paramaters:
	*		instance - Instance for using as 'this'.
	*	  instanceMethod - Method for calling.
  *   
	* Examples:
	*		$('a').bind('click', Function.createDelegate(this, this.clickHandler));
  */		
Function.createDelegate = function(instance, instanceMethod) {
	return function() {
		instanceMethod.apply(instance, arguments);
	}
};

/**
  *  Creates function callback.
  *   
	* Paramaters:
	*		instance - Instance for using as 'this'.
	*	  context - Method for calling.
  *   
	* Examples:
	*		$('a').bind('click', Function.createDelegate(this, this.clickHandler));
  */	
Function.createCallback = function(instanceMethod, context) {
	return function() {
		var argcount = arguments.length;
		if (argcount > 0) {
			var args = new Array(argcount + 1);
			for (var i = 0; i < argcount; i++) {
				args[i] = arguments[i];
			}
			args[argcount] = context;
			return instanceMethod.apply(this, args);
		}
		return instanceMethod.call(this, context);	
	}
};

/**
  *  Parses boolean value from it's string representation.
  *   
	* Paramaters:
	*		stringValue - string representation of boolean value.
  *   
	* Examples:
	*		Boolean.parse('true') => true
	*		Boolean.parse('false') => false
	*		Boolean.parse('True') => true
	*		Boolean.parse('False') => false	
  */	
Boolean.parse = function(stringValue) {
  return stringValue && stringValue.toLowerCase() == 'true';
};

/*
 * Date Format 1.2.2
 * (c) 2007-2008 Steven Levithan <stevenlevithan.com>
 * MIT license
 * Includes enhancements by Scott Trenda <scott.trenda.net> and Kris Kowal <cixar.com/~kris.kowal/>
 *
 * Accepts a date, a mask, or a date and a mask.
 * Returns a formatted version of the given date.
 * The date defaults to the current date/time.
 * The mask defaults to dateFormat.masks.default.
 */
var dateFormat = function () {
	var	token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
			val = String(val);
			len = len || 2;
			while (val.length < len) val = "0" + val;
			return val;
		};

	// Regexes and supporting functions are cached through closure
	return function (date, mask, utc) {
		var dF = dateFormat;

		// You can't provide utc if you skip other args (use the "UTC:" mask prefix)
		if (arguments.length == 1 && (typeof date == 'string' || date instanceof String) && !/\d/.test(date)) {
			mask = date;
			date = undefined;
		}

		// Passing date through Date applies Date.parse, if necessary
		date = date ? new Date(date) : new Date();
		if (isNaN(date)) throw new SyntaxError("invalid date");

		mask = String(dF.masks[mask] || mask || dF.masks["default"]);

		// Allow setting the utc argument via the mask
		if (mask.slice(0, 4) == "UTC:") {
			mask = mask.slice(4);
			utc = true;
		}

		var	_ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
				d:    d,
				dd:   pad(d),
				ddd:  dF.i18n.dayNames[D],
				dddd: dF.i18n.dayNames[D + 7],
				m:    m + 1,
				mm:   pad(m + 1),
				mmm:  dF.i18n.monthNames[m],
				mmmm: dF.i18n.monthNames[m + 12],
				yy:   String(y).slice(2),
				yyyy: y,
				h:    H % 12 || 12,
				hh:   pad(H % 12 || 12),
				H:    H,
				HH:   pad(H),
				M:    M,
				MM:   pad(M),
				s:    s,
				ss:   pad(s),
				l:    pad(L, 3),
				L:    pad(L > 99 ? Math.round(L / 10) : L),
				t:    H < 12 ? "a"  : "p",
				tt:   H < 12 ? "am" : "pm",
				T:    H < 12 ? "A"  : "P",
				TT:   H < 12 ? "AM" : "PM",
				Z:    utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
				o:    (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
				S:    ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

		return mask.replace(token, function ($0) {
			return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
		});
	};
}();

// Some common format strings
dateFormat.masks = {
	"default":      "ddd mmm dd yyyy HH:MM:ss",
	shortDate:      "m/d/yy",
	mediumDate:     "mmm d, yyyy",
	longDate:       "mmmm d, yyyy",
	fullDate:       "dddd, mmmm d, yyyy",
	shortTime:      "h:MM TT",
	mediumTime:     "h:MM:ss TT",
	longTime:       "h:MM:ss TT Z",
	isoDate:        "yyyy-mm-dd",
	isoTime:        "HH:MM:ss",
	isoDateTime:    "yyyy-mm-dd'T'HH:MM:ss",
	isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
	dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
	monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.advancedFormat = function (mask, utc) {
	return dateFormat(this, mask, utc);
};

/**
*
*  URL encode / decode
*  http://www.webtoolkit.info/
*
**/

Url = {

    // public method for url encoding
    encode : function (string) {
        return escape(this._utf8_encode(string));
    },

    // public method for url decoding
    decode : function (string) {
        return this._utf8_decode(unescape(string));
    },

    // private method for UTF-8 encoding
    _utf8_encode : function (string) {
        if (string) {
          string = string.replace(/\r\n/g,"\n");
          var utftext = "";

          for (var n = 0; n < string.length; n++) {

              var c = string.charCodeAt(n);

              if (c < 128) {
                  utftext += String.fromCharCode(c);
              }
              else if((c > 127) && (c < 2048)) {
                  utftext += String.fromCharCode((c >> 6) | 192);
                  utftext += String.fromCharCode((c & 63) | 128);
              }
              else {
                  utftext += String.fromCharCode((c >> 12) | 224);
                  utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                  utftext += String.fromCharCode((c & 63) | 128);
              }

          }
          
          return utftext;
        }
        else {
          return '';
        }
    },

    // private method for UTF-8 decoding
    _utf8_decode : function (utftext) {
        if (utftext) {
          var string = '';
          var i = 0;
          var c = c1 = c2 = 0;

          while ( i < utftext.length ) {

              c = utftext.charCodeAt(i);

              if (c < 128) {
                  string += String.fromCharCode(c);
                  i++;
              }
              else if((c > 191) && (c < 224)) {
                  c2 = utftext.charCodeAt(i+1);
                  string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                  i += 2;
              }
              else {
                  c2 = utftext.charCodeAt(i+1);
                  c3 = utftext.charCodeAt(i+2);
                  string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                  i += 3;
              }

          }

          return string;
       }
       else {
        return '';
       }
    }    

};