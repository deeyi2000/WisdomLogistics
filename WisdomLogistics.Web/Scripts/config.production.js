(function(root, factory) {
  factory([root])
})(this, function(_extern_lib_) {
  const root = _extern_lib_[0]
  let loadScripts = []
  new Promise((resolve, reject) => {
    let scripts = root.document.querySelectorAll('script[data-test]')
    let loadPromises = []
    for(var s of scripts) {
      if(!!s.attributes['data-test'] && !eval(s.attributes['data-test'].value) && !!s.attributes['data-src']) {
        loadPromises.push(new Promise((resolve, reject) => {
          let node = root.document.createElement('script')
          node.src = s.attributes['data-src'].value
          node.onload = node.onreadystatechange = () => {
            node.onload = node.onreadystatechange = null
            resolve()
          }
          s.parentNode.replaceChild(node, s)
        }))
      }
    }
    if(loadPromises.length > 0) {
      setTimeout(_ => {
        let scripts = root.document.querySelectorAll('script:not(:empty)')
        for(var s of scripts) {
          let placeholder = root.document.createComment('script')
          let script = root.document.createElement('script')
          script.innerHTML = s.innerHTML
          script.attributes = s.attributes
          loadScripts.push({placeholder, script})
          s.parentNode.replaceChild(placeholder, s)
        }
      }, 0)
    }

    resolve(Promise.all(loadPromises))
  }).then(_ => {
    if(root.SystemJS) {
      SystemJS.config({
        baseURL: '../addons/ewei_shop/static/js',
        pluginFirst: true,
        map: {
          //loader    
          'systemjs-babel-build': 'https://unpkg.com/systemjs-plugin-babel/systemjs-babel-browser.js',
          'babel-loader': 'https://unpkg.com/systemjs-plugin-babel/plugin-babel.js',
          'typescript': 'https://unpkg.com/typescript/lib/typescript.js',
          'ts-loader': 'https://unpkg.com/plugin-typescript/lib/plugin.js',
          'css': 'https://unpkg.com/systemjs-plugin-css/',
          'css-loader': 'https://unpkg.com/systemjs-plugin-css/css.js',
          'lesscss': 'https://unpkg.com/systemjs-plugin-less/less-browser.js',
          'less-loader': 'https://unpkg.com/systemjs-plugin-less/less.js',
          'json-loader': 'https://unpkg.com/systemjs-plugin-json/json.js',
          'text-loader': 'https://unpkg.com/systemjs-plugin-text/text.js',
          'image-loader': 'https://unpkg.com/system-image/image.js',
          //lib
          'babel-standalone': 'https://unpkg.com/babel-standalone@6/babel.min.js',
          'babel-polyfill': 'https://unpkg.com/babel-polyfill@6/dist/polyfill.min.js',
          'lodash': 'https://unpkg.com/lodash@4/lodash.min.js',
          'jquery3': 'https://unpkg.com/jquery@3/dist/jquery.min.js',
          'bootstrap3': 'https://unpkg.com/bootstrap@3/dist/js/bootstrap.min.js',
          'bootstrap3_css': 'https://unpkg.com/bootstrap@3/dist/css/bootstrap.min.css',
          'bootstrap3-theme_css': 'https://unpkg.com/bootstrap@3/dist/css/bootstrap-theme.min.css',
          'vue': 'https://unpkg.com/vue@2/dist/vue.min.js',
          'vue-router': 'https://unpkg.com/vue-router@2/dist/vue-router.min.js',
          'vuex': 'https://unpkg.com/vuex@2/dist/vuex.min.js',
          'axios': 'https://unpkg.com/axios/dist/axios.min.js',
          'element-ui': 'https://unpkg.com/element-ui/lib/index.js',
          'element-ui_css': 'https://unpkg.com/element-ui/lib/theme-default/index.css',
          'mint-ui': 'https://unpkg.com/mint-ui/lib/index.js',
          'mint-ui_css': 'https://unpkg.com/mint-ui/lib/style.min.css',
          'font-awesome': 'https://unpkg.com/font-awesome@4/css/font-awesome.min.css',
          'vue-amap': 'https://unpkg.com/vue-amap/dist/index.js',
          'vue-qriously': 'https://unpkg.com/vue-qriously/dist/vue-qriously.js',
          'jweixin': 'https://res.wx.qq.com/open/js/jweixin-1.2.0.js',
          //old lib
          'jquery': 'dist/jquery-1.11.1.min.js',
          'jquery.ui': 'dist/jquery-ui-1.10.3.min.js',
          'bootstrap': 'dist/bootstrap.min.js',
          'core':'app/core.js',
          'tpl':'dist/tmodjs.js',
          'jquery.touchslider':'dist/jquery.touchslider.min.js',
          'swipe':'dist/swipe.js',
          'sweetalert':'dist/sweetalert/sweetalert.min.js',
        },
        meta: {
          //loader
          '*.esm.js': {loader: 'babel-loader'},
          '*.ts': {loader: 'ts-loader'},
          '*.css': {loader: 'css-loader'},
          '*.less': {loader: 'less-loader'},
          '*.json': {loader: 'json-loader'},
          '*.txt': {loader: 'text-loader'},
          //lib
          'bootstrap3': {
            deps: ['bootstrap3_css', 'bootstrap-theme3_css', 'font-awesome'],
          },
          'element-ui': {
            deps: ['element-ui_css', 'font-awesome'],
            format: 'global', exports: 'ELEMENT'
          },
          'mint-ui': {
            deps: ['mint-ui_css', 'font-awesome'],
            format: 'global', exports: 'MINT'
          },
          'vue-amap': {
            deps: [],  //must has deps[], otherwise can't exports ??!!
            format: 'global', exports: 'VueAMap'
          },
          'jweixin': {
            scriptLoad: true
          },
          //old lib
          'jquery.ui': {
            deps: ['jquery'],
            format: 'global', exports: "$"
          },
          'bootstrap': {
            deps: ['jquery'],
            format: 'global', exports: "$"
          },  
          'jquery.touchslider': {
            deps: ['jquery'],
            format: 'global', exports: "$"
          },
          'sweetalert':{
            deps: ['dist/sweetalert/sweetalert.css'],
            format: 'global',  exports: "$"
          }
        }
      })
      root.define || (root.define = SystemJS.amdDefine)
      root.require || (root.require = root.requirejs = SystemJS.amdRequire)
    }

    if(root.FastClick) {
      root.document.addEventListener('DOMContentLoaded', function() {
          root.FastClick.attach(document.body);
      }, false);
    }

    if(root.Vue && root.axios) {
      Vue.$axios = axios.create({headers: {'X-Requested-With': 'XMLHttpRequest'}})
      Object.defineProperties(Vue.prototype, {
        $axios: {get() { return Vue.$axios }},
        $http: {get() { return Vue.$axios }},
      })
    }

    return Promise.resolve()
  }).then(_ => {
    for(var ls of loadScripts) {
      ls.placeholder.parentNode.replaceChild(ls.script, ls.placeholder)
    }
  })
})