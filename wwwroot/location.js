function initMap() {
    if (document.getElementById('latlong')) {
        console.log("here1");
        document.getElementById('latlong').innerHTML = "here2";

        var card = document.getElementById('pac-card');
        var input = document.getElementById('pac-input');
        var types = document.getElementById('type-selector');
        var strictBounds = document.getElementById('strict-bounds-selector');

        var autocomplete = new google.maps.places.Autocomplete(input);
        autocomplete.setTypes(['geocode']);

        console.log("here3");
        // Set the data fields to return when the user selects a place.
        autocomplete.setFields(
            ['address_components', 'geometry', 'icon', 'name']);

        autocomplete.addListener('place_changed', function () {
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                // User entered the name of a Place that was not suggested and
                // pressed the Enter key, or the Place Details request failed.
                window.alert("No details available for input: '" + place.name + "'");
                return;
            }

            var address = '';
            if (place.address_components) {
                address = [
                    (place.address_components[0] && place.address_components[0].short_name || ''),
                    (place.address_components[1] && place.address_components[1].short_name || ''),
                    (place.address_components[2] && place.address_components[2].short_name || '')
                ].join(' ');

            }
            document.getElementById('latlong').innerHTML = "Lat: " + place.geometry.location.lat() + ",Long: " + place.geometry.location.lng();
        });
    } else {
        console.log("not in location");
    }
}