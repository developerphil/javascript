module app.widgets {
    'use strict';

    interface ISlugCheckController {
        checkSlugInUse(slug: string): ng.IPromise<boolean>;
    }

    class SlugCheckController implements ISlugCheckController {
        static $inject = ['app.services.BlogPostService'];
        constructor(private blogPostService: app.services.IBlogPostService) {
        }

        checkSlugInUse(slug: string): ng.IPromise<boolean> {
            return this.blogPostService.checkSlugInUse(slug);
        }
    }

    interface IAttributes extends ng.IAttributes {
        blSlugCheck: string;
        minLengthToCheck: number;
    }
    class SlugCheckDirective implements ng.IDirective {
        static instance(): ng.IDirective {
            return new SlugCheckDirective;
        }

        restrict = 'A';
        controller = SlugCheckController;

        link(scope: ng.IScope, element: ng.IAugmentedJQuery,
            attributes: IAttributes, controller: ISlugCheckController): void {
            element.on('blur', (): void => {
                controller.checkSlugInUse(element.val())
                    .then((inUse: boolean): void => {
                        console.log(inUse);
                    });
            });
        }
    }

    angular
        .module('app.widgets')
        .directive('blSlugCheck', SlugCheckDirective.instance);

    slugCheck.$inject = ['app.services.BlogPostService'];
    function slugCheck(blogPostService: app.services.IBlogPostService): ng.IDirective {
        var directive = <ng.IDirective> {
            restrict: 'A',
            link: link,
            controller: SlugCheckController
        };

        function link(scope: ng.IScope, element: ng.IAugmentedJQuery): void {
            element.on('blur', (): void => {
                blogPostService.checkSlugInUse(element.val())
                    .then((inUse: boolean): void => {
                        console.log(inUse);
                    });
            });
        }

        return directive;
    }
}