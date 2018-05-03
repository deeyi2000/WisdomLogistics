'use strict';

(function(root, factory) {
  factory([root])
})(this, function(_extern_lib_) {
  const root = _extern_lib_[0]

  //定义一些常量
　var x_PI = 3.14159265358979324 * 3000.0 / 180.0;
　var PI = 3.1415926535897932384626;
　var a = 6378245.0;
　var ee = 0.00669342162296594323;
　/**
　* 百度坐标系 (BD-09) 与 火星坐标系 (GCJ-02)的转换
　* 即 百度 转 谷歌、高德
　* @param bd_lon
　* @param bd_lat
　* @returns {*[]}
　*/
  root.bd09togcj02 = function(bd_lon, bd_lat) { 
　  var x_pi = 3.14159265358979324 * 3000.0 / 180.0;
　  var x = bd_lon - 0.0065;
　  var y = bd_lat - 0.006;
　  var z = Math.sqrt(x * x + y * y) - 0.00002 * Math.sin(y * x_pi);
　  var theta = Math.atan2(y, x) - 0.000003 * Math.cos(x * x_pi);
　  var gg_lng = z * Math.cos(theta);
　  var gg_lat = z * Math.sin(theta);
　  return [gg_lng, gg_lat]
　}
  /**
  * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换
  * 即谷歌、高德 转 百度
  * @param lng
  * @param lat
  * @returns {*[]}
  */
  root.gcj02tobd09 = function(lng, lat) { 
    var z = Math.sqrt(lng * lng + lat * lat) + 0.00002 * Math.sin(lat * x_PI);
    var theta = Math.atan2(lat, lng) + 0.000003 * Math.cos(lng * x_PI);
    var bd_lng = z * Math.cos(theta) + 0.0065;
    var bd_lat = z * Math.sin(theta) + 0.006;
    return [bd_lng, bd_lat]
  }
  /**
  * WGS84转GCj02
  * @param lng
  * @param lat
  * @returns {*[]}
  */
  root.wgs84togcj02 = function(lng, lat) { 
    if (out_of_china(lng, lat)) {
      return [lng, lat]
    }
    else {
      var dlat = transformlat(lng - 105.0, lat - 35.0);
      var dlng = transformlng(lng - 105.0, lat - 35.0);
      var radlat = lat / 180.0 * PI;
      var magic = Math.sin(radlat);
      magic = 1 - ee * magic * magic;
      var sqrtmagic = Math.sqrt(magic);
      dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
      dlng = (dlng * 180.0) / (a / sqrtmagic * Math.cos(radlat) * PI);
      var mglat = lat + dlat;
      var mglng = lng + dlng;
      return [mglng, mglat]
    }
  }
  /**
  * GCJ02 转换为 WGS84
  * @param lng
  * @param lat
  * @returns {*[]}
  */
  root.gcj02towgs84 = function(lng, lat) { 
    if (out_of_china(lng, lat)) {
      return [lng, lat]
    }
    else {
      var dlat = transformlat(lng - 105.0, lat - 35.0);
      var dlng = transformlng(lng - 105.0, lat - 35.0);
      var radlat = lat / 180.0 * PI;
      var magic = Math.sin(radlat);
      magic = 1 - ee * magic * magic;
      var sqrtmagic = Math.sqrt(magic);
      dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
      dlng = (dlng * 180.0) / (a / sqrtmagic * Math.cos(radlat) * PI);
      mglat = lat + dlat;
      mglng = lng + dlng;
      return [lng * 2 - mglng, lat * 2 - mglat]
    }
  }

  function transformlat(lng, lat) { 
  var ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.sqrt(Math.abs(lng));
  ret += (20.0 * Math.sin(6.0 * lng * PI) + 20.0 * Math.sin(2.0 * lng * PI)) * 2.0 / 3.0;
  ret += (20.0 * Math.sin(lat * PI) + 40.0 * Math.sin(lat / 3.0 * PI)) * 2.0 / 3.0;
  ret += (160.0 * Math.sin(lat / 12.0 * PI) + 320 * Math.sin(lat * PI / 30.0)) * 2.0 / 3.0;
  return ret
  }
  function transformlng(lng, lat) { 
  var ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.sqrt(Math.abs(lng));
  ret += (20.0 * Math.sin(6.0 * lng * PI) + 20.0 * Math.sin(2.0 * lng * PI)) * 2.0 / 3.0;
  ret += (20.0 * Math.sin(lng * PI) + 40.0 * Math.sin(lng / 3.0 * PI)) * 2.0 / 3.0;
  ret += (150.0 * Math.sin(lng / 12.0 * PI) + 300.0 * Math.sin(lng / 30.0 * PI)) * 2.0 / 3.0;
  return ret
  }
  /**
  * 判断是否在国内，不在国内则不做偏移
  * @param lng
  * @param lat
  * @returns {boolean}
  */
  function out_of_china(lng, lat) { 
  return (lng < 72.004 || lng > 137.8347) || ((lat < 0.8293 || lat > 55.8271) || false);
  }

  /* 对Date的扩展，将 Date 转化为指定格式的String * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q)
  * 可以用 1-2 个占位符 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) * eg: * (new
  * Date()).format("yyyy-MM-dd hh:mm:ss.S")==> 2006-07-02 08:09:04.423
  * (new Date()).format("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04
  * (new Date()).format("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04
  * (new Date()).format("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04
  * (new Date()).format("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18
  */
  Date.prototype.format=function(fmt) {
    var o = {
    "M+" : this.getMonth()+1, //月份
    "d+" : this.getDate(), //日
    "h+" : this.getHours()%12 == 0 ? 12 : this.getHours()%12, //小时
    "H+" : this.getHours(), //小时
    "m+" : this.getMinutes(), //分
    "s+" : this.getSeconds(), //秒
    "q+" : Math.floor((this.getMonth()+3)/3), //季度
    "S" : this.getMilliseconds() //毫秒
    };
    var week = {
    "0" : "/u65e5",
    "1" : "/u4e00",
    "2" : "/u4e8c",
    "3" : "/u4e09",
    "4" : "/u56db",
    "5" : "/u4e94",
    "6" : "/u516d"
    };
    if(/(y+)/.test(fmt)){
      fmt=fmt.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));
    }
    if(/(E+)/.test(fmt)){
      fmt=fmt.replace(RegExp.$1, ((RegExp.$1.length>1) ? (RegExp.$1.length>2 ? "/u661f/u671f" : "/u5468") : "")+week[this.getDay()+""]);
    }
    for(var k in o){
      if(new RegExp("("+ k +")").test(fmt)){
        fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));
      }
    }
    return fmt;
  }

  // If obj.hasOwnProperty has been overridden, then calling
  // obj.hasOwnProperty(prop) will break.
  // See: https://github.com/joyent/node/issues/1707
  function hasOwnProperty(obj, prop) {
    return Object.prototype.hasOwnProperty.call(obj, prop);
  }

  function stringifyPrimitive(v) {
    switch (typeof v) {
      case 'string': return v;
      case 'boolean': return v ? 'true' : 'false';
      case 'number': return isFinite(v) ? v : '';
      default: return '';
    }
  }
  root.querystring = {    
    encode: function(obj, name, sep, eq) {
      sep = sep || '&'
      eq = eq || '='
      obj = obj || undefined

      if ((typeof obj === 'object') || Array.isArray(obj)) {
        obj = Object.keys(obj).map(k => querystring.encode(obj[k], !!name ? `${name}[${stringifyPrimitive(k)}]` : `${k}`, sep, eq)).join(sep);
      }
      else {
        obj = obj || ''
        obj = !!name ? `${name}${eq}${obj}` : ''
      }
      return obj
    },
    decode: function(qs, sep, eq, options) {
      sep = sep || '&';
      eq = eq || '=';
      var obj = {};
    
      if (typeof qs !== 'string' || qs.length === 0) {
        return obj;
      }
    
      var regexp = /\+/g;
      qs = qs.split(sep);
    
      var maxKeys = 1000;
      if (options && typeof options.maxKeys === 'number') {
        maxKeys = options.maxKeys;
      }
    
      var len = qs.length;
      // maxKeys <= 0 means that we should not limit keys count
      if (maxKeys > 0 && len > maxKeys) {
        len = maxKeys;
      }
    
      for (var i = 0; i < len; ++i) {
        var x = qs[i].replace(regexp, '%20'),
            idx = x.indexOf(eq),
            kstr, vstr, k, v;
    
        if (idx >= 0) {
          kstr = x.substr(0, idx);
          vstr = x.substr(idx + 1);
        } else {
          kstr = x;
          vstr = '';
        }
    
        k = decodeURIComponent(kstr);
        v = decodeURIComponent(vstr);
    
        if (!hasOwnProperty(obj, k)) {
          obj[k] = v;
        } else if (Array.isArray(obj[k])) {
          obj[k].push(v);
        } else {
          obj[k] = [obj[k], v];
        }
      }
    
      return obj;
    }
  }

  root.debounce = function(func, timeout) {
      var last;
    return function() {
        var ctx = this, args = arguments;
        if (last) clearTimeout(last);
        last = setTimeout(function() { func.apply(ctx, args) }, timeout);
    }
  }

  root.throttle = function(func, delay){
    var last = 0
    return function(){
      var curr = Date.now()
      if (curr - last > delay){
        func.apply(this, arguments)
        last = curr 
      }
    }
  }
})
  