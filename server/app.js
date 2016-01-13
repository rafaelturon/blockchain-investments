// call packages
var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var serveStatic = require('serve-static');
var morgan     = require('morgan');

// configure app
app.use(morgan('dev')); // log requests to the console

// body-parser config
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// set app port
var port = process.env.PORT || 8080;

// set database
var mongoose   = require('mongoose');
mongoose.connect(process.env.MONGOLAB_URI, function (error) {
    if (error) console.error(error);
    else console.log('mongo connected');
});

// STATIC VERSION
// ==============
app.use(serveStatic('server/dist', {'index': ['index.html', 'default.htm']}));

// ROUTES FOR API
// ==============
var router = express.Router();

// Default GET /api
router.get('/', function(req, res){
    res.json( {message: 'API Version: X.X.X.X'});
});

// REGISTER ROUTER
app.use('/api', router);

// START SERVER
// ============
app.listen(port, function() {
    console.log('Our app is running on http://localhost:' + port);
});

module.exports = app;