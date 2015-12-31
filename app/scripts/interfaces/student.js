'use strict';
var kmpoint;
(function (kmpoint) {
    var Student = (function () {
        function Student(name, email, isSubscriber, age) {
            this.name = name;
            this.email = email;
            this.isSubscriber = isSubscriber;
            this.age = age;
        }
        Student.prototype.register = function () {
            console.log('Aluno cadastrado com sucesso!');
        };
        return Student;
    })();
    kmpoint.Student = Student;
})(kmpoint || (kmpoint = {}));
//# sourceMappingURL=student.js.map