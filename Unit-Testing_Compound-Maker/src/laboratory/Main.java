package laboratory;
//	
import java.io.File;
import java.io.IOException;
import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.stage.Stage;
import javafx.scene.Cursor;
import javafx.scene.Group;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.control.Tooltip;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.scene.media.Media;
import javafx.scene.media.MediaPlayer;
import javafx.scene.media.MediaView;
import javafx.scene.paint.Color;
import javafx.scene.text.TextAlignment;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Pos;


public class Main extends Application {
	
	
	@Override
	public void start(Stage primaryStage) throws Exception {

	//	setUserAgentStylesheet(STYLESHEET_CASPIAN);
		
		Button btnLogin = new Button("Login");
		btnLogin.setStyle("-fx-background-color: green;");
		Button btnGuest = new Button("Login as Guest");
		VBox box = new VBox(1);
		HBox innerBox = new HBox(4);
		box.setStyle("-fx-background-color: black;");

//		Label l = new Label();
//		box.getChildren().add(l);
//		Label notOurVideo = new Label(" Video taken from \"Beauty of Science\"  https://www.youtube.com/watch?v=dQGreQyoXxI&t=139s ");
//      box.getChildren().add(notOurVideo);
		Label loginLabel = new Label(" UserName: ");

		loginLabel.setTextFill(Color.DARKGRAY);
		TextField userNameTF = new TextField();
		userNameTF.setTooltip(new Tooltip("Please enter your name"));
		userNameTF.setStyle("-fx-background-color: darkgrey");
		
		Label passwordLabel = new Label();
		passwordLabel.setTextFill(Color.DARKGRAY);
		passwordLabel.setText("Password: ");
		
		PasswordField password = new PasswordField();
		password.setTooltip(new Tooltip("Optional"));
		password.setStyle("-fx-background-color: darkgrey");
		
		btnGuest.setCursor(Cursor.CLOSED_HAND);
		btnLogin.setCursor(Cursor.CLOSED_HAND);
        
        innerBox.getChildren().addAll(loginLabel,userNameTF,passwordLabel,password, btnGuest,btnLogin);
        box.getChildren().add(innerBox);
       

        Scene scene = new Scene(box, 1900, 1100);
//        scene.getStylesheets().add(getClass().getResource("application.css").toExternalForm());
        File file = new File("loginScreen.mp4");
        
        /*
         * Depending on if you have openjfx or jdk 8 you may need to add these.
         * 
         * Copy the path below (Where openjfx is on your computer):
         * 
         * 
         *  
         *  --module-path "C:\Java\openjfx-11.0.2_windows-x64_bin-sdk\javafx-sdk-11.0.2\lib" --add-modules javafx.swing,javafx.graphics,javafx.fxml,javafx.media,javafx.web --add-reads javafx.graphics=ALL-UNNAMED --add-opens javafx.controls/com.sun.javafx.charts=ALL-UNNAMED --add-opens javafx.graphics/com.sun.javafx.iio=ALL-UNNAMED --add-opens javafx.graphics/com.sun.javafx.iio.common=ALL-UNNAMED --add-opens javafx.graphics/com.sun.javafx.css=ALL-UNNAMED --add-opens javafx.base/com.sun.javafx.runtime=ALL-UNNAMED 
         * 
         * 
         * The modules and other junk are also needed if you are not using jdk 8
         * Then go to -> Run Configuration -> Arguments and in the VM Arguments textfield paste in the box
         * 
         */
        Media media = new Media(file.toURI().toString());
        MediaPlayer player = new MediaPlayer(media);
        player.setCycleCount(MediaPlayer.INDEFINITE);
        
   
        //This mediaView is added to a Pane
        MediaView view = new MediaView(player);
        player.play();

        btnGuest.setOpacity(0.5);
        btnGuest.setCenterShape(true);
        ((VBox)scene.getRoot()).getChildren().addAll(view);

        
        //show the stage
        primaryStage.setTitle("MyChemicalGarden");
        primaryStage.setScene(scene);

        primaryStage.show();
        
        btnLogin.setOnAction(new EventHandler<ActionEvent>() {
			
			@Override
			public void handle(ActionEvent event) {
				// TODO Auto-generated method stub
				String name = userNameTF.getText();
		        Parent view2;
				try {
					player.setVolume(0.0);
					view2 = FXMLLoader.load(getClass().getResource("ScienceLab.fxml"));
					Scene scene2 = new Scene(view2);
					scene2.getStylesheets().add(getClass().getResource("application.css").toExternalForm());
				    Stage window = new Stage();
				    window.setTitle("Hello " + name +"! Welcome to MyChemicalGarden");
				    window.setScene(scene2);
				    Stage stage = (Stage) btnGuest.getScene().getWindow();
				    stage.close();
				    window.show();
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
		      
				
			}
		});

        btnGuest.setOnAction(new EventHandler<ActionEvent>() {
			
			@Override
			public void handle(ActionEvent event) {
				// TODO Auto-generated method stub
		        Parent view2;
				try {
					player.setVolume(0.0);
					view2 = FXMLLoader.load(getClass().getResource("ScienceLab.fxml"));
					Scene scene2 = new Scene(view2);
					scene2.getStylesheets().add(getClass().getResource("application.css").toExternalForm());
				    Stage window = new Stage();
				    window.setTitle("MyChemGarden");
				    window.setScene(scene2);
				    Stage stage = (Stage) btnGuest.getScene().getWindow();
				    stage.close();
				    window.show();
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
		      
				
			}
		});

	}
	
	public static void main(String[] args) {
		launch(args);
	}
}
