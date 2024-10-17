function select2Initializer(selector, url, type,parent) {
    $(selector).select2({
        dropdownParent: (parent && $(parent).length) ? $(parent) : null,
        ajax: {
            url: url,
            
            data: function (params) {
                var query = {
                    searchTerm: params.term,
                    type: type 
                }
                return query;
            },
            processResults: function (data) {
                // Transforms the top-level key of the response object from 'items' to 'results'
                //console.log('Data dekhi:', data);                  
                return {
                    results: data
                };
            }
        }
    });
   
}

function DefaultSelected(selector, value, text) {
    var data =
    {
        id: value==null?"Please Select":value,
        text: text == null ? "Please Select" : text
    };

    var dropdown = $(selector);
    var option = new Option(data.text, data.id,true, true);
    dropdown.append(option).trigger('change');

    //// manually trigger the `select2:select` event
    dropdown.trigger({
        type: 'select2:select',
        params: {
            data: data
        }
    });
}