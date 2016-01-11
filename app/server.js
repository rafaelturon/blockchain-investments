var express = require('express');
var app = express();
var serveStatic = require('serve-static');

var port = process.env.PORT || 8080;
app.use(serveStatic('dist', {'index': ['index.html', 'default.htm']}));

app.listen(port, function() {
    console.log('Our app is running on http://localhost:' + port);
});