(function(root, factory) {
  factory([root])
})(this, function(_extern_lib_) {
  var root = _extern_lib_[0],
      loadScripts = []
  new Promise(function(resolve, reject) {
    var scripts = root.document.querySelectorAll('script[data-test]')
    var loadPromises = []
    for(var i = 0; i < scripts.length; i++) {
      var s = scripts[i]
      if(s.hasAttribute('data-test') && !eval(s.getAttribute('data-test')) && s.hasAttribute('data-src')) {
        loadPromises.push(new Promise(function(resolve, reject) {
          var node = root.document.createElement('script')
          node.src = s.attributes['data-src'].value
          node.onload = node.onreadystatechange = function() {
            node.onload = node.onreadystatechange = null
            resolve()
          }
          s.parentNode.replaceChild(node, s)
        }))
      }
    }
    if('ActiveXObject' in window || loadPromises.length > 0) {
      setTimeout(function() {
        var scripts = root.document.querySelectorAll('script:not(:empty)')
        for(var i = 0; i < scripts.length; i++) {
          var s = scripts[i]
          var placeholder = root.document.createComment('script')
          var script = root.document.createElement('script')
          script.innerHTML = s.innerHTML
          script.attributes = s.attributes
          loadScripts.push({'placeholder': placeholder, 'script': script})
          s.parentNode.replaceChild(placeholder, s)
        }
      }, 0)
    }

    resolve(Promise.all(loadPromises))
  }).then(function() {
    if(root.SystemJS) {
      root.SystemJS.config({
        baseURL: './lib/',
        pluginFirst: true,
        map: {
          //loader    
          'systemjs-babel-build': 'https://unpkg.com/systemjs-plugin-babel/systemjs-babel-browser.js',
          'babel-loader': 'https://unpkg.com/systemjs-plugin-babel/plugin-babel.js',
          'typescript': 'https://unpkg.com/typescript@2.5.3/lib/typescript.js',
          'ts-loader': 'https://unpkg.com/plugin-typescript/lib/plugin.js',
          'coffee-script': 'https://unpkg.com/system-coffee/coffee-script.js',
          'coffee-loader': 'https://unpkg.com/system-coffee/coffee.js',
          'css': 'https://unpkg.com/systemjs-plugin-css/',
          'css-loader': 'https://unpkg.com/systemjs-plugin-css/css.js',
          'lesscss': 'https://unpkg.com/systemjs-plugin-less/less-browser.js',
          'less-loader': 'https://unpkg.com/systemjs-plugin-less/less.js',
          'json-loader': 'https://unpkg.com/systemjs-plugin-json/json.js',
          'text-loader': 'https://unpkg.com/systemjs-plugin-text/text.js',
          'image-loader': 'https://unpkg.com/system-image/image.js',
          //lib
          'fastclick': 'https://unpkg.com/fastclick@1/lib/fastclick.js',
          'babel-standalone': 'https://unpkg.com/babel-standalone@6/babel.min.js',
          'babel-polyfill': 'https://unpkg.com/babel-polyfill@6/dist/polyfill.min.js',
          'lodash': 'https://unpkg.com/lodash@4/lodash.min.js',
          'jquery': 'https://unpkg.com/jquery@3/dist/jquery.min.js',
          'bootstrap': 'https://unpkg.com/bootstrap@3/dist/js/bootstrap.min.js',
          'bootstrap_css': 'https://unpkg.com/bootstrap@3/dist/css/bootstrap.min.css',
          'bootstrap-theme_css': 'https://unpkg.com/bootstrap@3/dist/css/bootstrap-theme.min.css',
          'vue': 'https://unpkg.com/vue@2/dist/vue.min.js',
          'vue-router': 'https://unpkg.com/vue-router@2/dist/vue-router.min.js',
          'vuex': 'https://unpkg.com/vuex@2/dist/vuex.min.js',
          'axios': 'https://unpkg.com/axios/dist/axios.min.js',
          'element-ui': 'https://unpkg.com/element-ui/lib/index.js',
          'element-ui_css': 'https://unpkg.com/element-ui/lib/theme-default/index.css',
          'mint-ui': 'https://unpkg.com/mint-ui/lib/index.js',
          'mint-ui_css': 'https://unpkg.com/mint-ui/lib/style.min.css',
          'we-vue': 'https://unpkg.com/we-vue/lib/index.js',
          'we-vue_css': 'https://unpkg.com/we-vue/lib/style.min.css',
          'vux': 'https://unpkg.com/vux-dist/vux.min.js',
          'vux_css': 'https://unpkg.com/vux-dist/vux.min.css',
          'font-awesome': 'https://unpkg.com/font-awesome@4/css/font-awesome.min.css',
          'vue-amap': 'https://unpkg.com/vue-amap/dist/index.js',
          'vue-qriously': 'https://unpkg.com/vue-qriously/dist/vue-qriously.js',
          'vue-markdown': 'https://unpkg.com/vue-markdown/dist/vue-markdown.common.js',
          'vue-pdf': 'https://unpkg.com/pdfjs-dist/build/pdf.min.js',
          'animate.css': 'https://unpkg.com/animate.css@3.5.2/animate.min.css',
          'jweixin': 'https://res.wx.qq.com/open/js/jweixin-1.2.0.js',
        },
        meta: {
          //loader
          '*.esm.js': {loader: 'babel-loader'},
          '*.ts': {loader: 'ts-loader'},
          '*.coffee': {loader: 'coffee-loader'},
          '*.css': {loader: 'css-loader'},
          '*.less': {loader: 'less-loader'},
          '*.json': {loader: 'json-loader'},
          '*.txt': {loader: 'text-loader'},
          //lib
          'bootstrap': {
            deps: ['bootstrap_css', 'bootstrap-theme_css', 'font-awesome'],
          },
          'element-ui': {
            deps: ['element-ui_css', 'font-awesome'],
            format: 'global', exports: 'ELEMENT'
          },
          'mint-ui': {
            deps: ['mint-ui_css', 'font-awesome'],
            format: 'global', exports: 'MINT'
          },
          'we-vue': {
            deps: ['we-vue_css', 'font-awesome'],
            format: 'global', exports: 'WE-VUE'
          },
          'vux': {
            deps: ['vux_css', 'font-awesome'],
          },
          'vue-amap': {
            deps: [],  //must has deps[], otherwise can't exports ??!!
            format: 'global', exports: 'VueAMap'
          },
          'jweixin': {
            scriptLoad: true
          },
        }
      })
      root.define || (root.define = root.SystemJS.amdDefine)
      root.require || (root.require = root.SystemJS.amdRequire)
    }
    
    if(root.FastClick) {
      root.document.addEventListener('DOMContentLoaded', function() {
          root.FastClick.attach(document.body);
      }, false);
    }

    if(root.Vue && root.axios) {
      Vue.$axios = root.axios.create({headers: {'X-Requested-With': 'XMLHttpRequest'}})
      Object.defineProperties(Vue.prototype, {
        $axios: {get: function() { return Vue.$axios }},
        $http: {get: function() { return Vue.$axios }},
      })
    }

    return Promise.resolve()
  }).then(function() {
    for(var i = 0; i < loadScripts.length; i++) {
      var ls = loadScripts[i]
      ls.placeholder.parentNode.replaceChild(ls.script, ls.placeholder)
    }
  })
})